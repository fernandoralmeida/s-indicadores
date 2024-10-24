var _max = 0;
var _min = 0;
var _url_zoomtofeatures = '';
// Criar o mapa Leaflet
// Centro inicial do mapa do estado de sp
let map = L.map('map', {
    maxZoom: 15,
    minZoom: 6,
    zoomControl: false
}).setView([-22.5665, -48.6353], 7);

var loadingMapDiv = document.getElementById('loadingMap');
// Tornar a div visível alterando a propriedade display
loadingMapDiv.classList.add("visible-on");

var info = L.control();

info.onAdd = function (map) {
    this._div = L.DomUtil.create('div', 'info'); // create a div with a class "info"
    this.update();
    return this._div;
};

// method that we will use to update the control based on feature properties passed
info.update = function (props) {
    this._div.innerHTML = (props ?
        'Município: <span class="text-uppercase">' + props.name + '</span><br /><div class="divider"></div>Empresas: ' + props.empresas + ' (' + _max + ') ' + '<br />Setor: ' + props.setor : 'Mova o mouse sobre o mapa');
};

info.addTo(map);

L.control.zoom({
    position: 'bottomleft'
}).addTo(map);

// Adicionar uma camada base do OpenStreetMap
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '© OpenStreetMap contributors'
}).addTo(map);

// Adiciona o tile layer escuro usando CartoDB Dark Matter
// L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
//     attribution: '&copy; <a href="https://carto.com/">CartoDB</a> contributors'
// }).addTo(map);

function highlightFeature(e) {
    var layer = e.target;

    layer.setStyle({
        weight: 3
    });

    layer.bringToFront();
    info.update(layer.feature.properties);
}

function resetHighlight(e) {
    //geojson.resetStyle(e.target);
    var layer = e.target;

    layer.setStyle({
        weight: 1
    });

    info.update();
}

// Função para carregar dados do IndexedDB
function loadGeoJSONFromIndexedDB() {
    return new Promise((resolve, reject) => {
        var request = indexedDB.open('geojsonDB', 1);

        request.onupgradeneeded = function (event) {
            var db = event.target.result;
            db.createObjectStore('geojsonStore', { keyPath: 'id' });
        };

        request.onsuccess = function (event) {
            var db = event.target.result;
            var transaction = db.transaction(['geojsonStore'], 'readonly');
            var objectStore = transaction.objectStore('geojsonStore');
            var request = objectStore.get('cachedGeoJSON');

            request.onsuccess = function (event) {
                var cachedData = event.target.result;
                if (cachedData) {
                    resolve(cachedData.data);
                } else {
                    reject('Dados GeoJSON não encontrados no IndexedDB.');
                }
            };

            request.onerror = function (event) {
                reject('Erro ao obter dados do IndexedDB.');
            };
        };

        request.onerror = function (event) {
            reject('Erro ao abrir o IndexedDB.');
        };
    });
}

// Carregar dados GeoJSON
function loadData(_url) {
    // Verificar se os dados estão no localStorage e se a última atualização foi em um dia diferente

    var lastUpdateDate = localStorage.getItem('lastUpdateDate');
    var currentDate = new Date().toLocaleDateString();

    if (!lastUpdateDate || lastUpdateDate !== currentDate) {
        // Dados não encontrados no localStorage ou último update não foi hoje, carregar dados da API
        fetchDataFromAPI(_url);
    } else {
        // Dados encontrados no localStorage e último update foi hoje, carregar dados do IndexedDB
        loadGeoJSONFromIndexedDB()
            .then(data => {
                // Dados encontrados no IndexedDB, processar os dados
                _max = data.max;
                processData(data);
            })
            .catch(error => {
                // Dados não encontrados no IndexedDB ou erro, carregar dados da API
                fetchDataFromAPI(_url);
            })
            .finally(() => {
                // Código a ser executado sempre, independentemente do sucesso ou falha da requisição
                loadingMapDiv.classList.remove("visible-on");
                loadingMapDiv.classList.add("visible-off");;
            });
    }
}

// Função para carregar dados da API
function fetchDataFromAPI(_api_url) {
    fetch(_api_url)
        .then(response => response.json())
        .then(data => {
            // Armazenar dados no IndexedDB para uso futuro
            storeGeoJSONInIndexedDB(data);

            // Armazenar a data da última atualização no localStorage
            localStorage.setItem('lastUpdateDate', new Date().toLocaleDateString());

            // Processar os dados GeoJSON
            _max = data.max;
            processData(data);
        })
        .catch(error => console.error('Erro ao carregar dados GeoJSON da API:', error))
        .finally(() => {
            // Código a ser executado sempre, independentemente do sucesso ou falha da requisição
            loadingMapDiv.classList.remove("visible-on");
            loadingMapDiv.classList.add("visible-off");;
        });
}

// Função para armazenar dados no IndexedDB
function storeGeoJSONInIndexedDB(data) {
    var request = indexedDB.open('geojsonDB', 1);

    request.onupgradeneeded = function (event) {
        var db = event.target.result;
        db.createObjectStore('geojsonStore', { keyPath: 'id' });
    };

    request.onsuccess = function (event) {
        var db = event.target.result;
        var transaction = db.transaction(['geojsonStore'], 'readwrite');
        var objectStore = transaction.objectStore('geojsonStore');
        objectStore.put({ id: 'cachedGeoJSON', data: data });
    };

    request.onerror = function (event) {
        console.error('Erro ao armazenar dados no IndexedDB.');
    };
}

// Função para processar os dados GeoJSON
function processData(data) {
    // $("#loader").hide();
    // $(".blackboxload").fadeOut("slow");

    // Adicionar camada GeoJSON ao mapa
    var geojsonLayer = L.geoJSON(data, {
        style: style,
        onEachFeature: onEachFeature
    }).addTo(map);

    legend.addTo(map);

    // Ajustar o mapa para abranger a área GeoJSON com zoom
    map.fitBounds(geojsonLayer.getBounds());
}