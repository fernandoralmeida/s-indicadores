// Crie o mapa
var map = new ol.Map({
    target: 'map',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],
    view: new ol.View({
        center: ol.proj.fromLonLat([0, 0]),
        zoom: 12
    })
});

var c_lat = 0;
var c_lon = 0;

// Função para obter coordenadas geográficas a partir do nome da cidade
function geocodeCityAndInitializeMap(map, cityName) {
    // Construa a URL do serviço de geocodificação (Nominatim)
    var url = 'https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(cityName);

    // Faça uma solicitação AJAX para obter os resultados de geocodificação
    fetch(url)
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                // Obtenha as coordenadas geográficas do primeiro resultado
                c_lon = parseFloat(data[0].lon);
                c_lat = parseFloat(data[0].lat);

                // Crie um marcador e adicione-o ao mapa
                /*
                var marker = new ol.Feature({
                    geometry: new ol.geom.Point(ol.proj.fromLonLat([c_lon, c_lat]))
                });

                var vectorSource = new ol.source.Vector({
                    features: [marker]
                });

                var vectorLayer = new ol.layer.Vector({
                    source: vectorSource
                });

                map.addLayer(vectorLayer);
                */

                // Ajuste o centro do mapa para o marcador
                map.getView().setCenter(ol.proj.fromLonLat([c_lon, c_lat]));
                map.getView().setZoom(14);
            } else {
                console.error('Nenhum resultado de geocodificação encontrado para a cidade:', cityName);
            }
        })
        .catch(error => {
            console.error('Erro ao obter resultados de geocodificação:', error);
        });
};

// Função para geocodificar um endereço e adicionar um marcador ao mapa
function geocodeAndAddMarker(map, address) {
    // Construa a URL do serviço de geocodificação (Nominatim)
    var url = 'https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(address);

    // Faça uma solicitação AJAX para obter os resultados de geocodificação
    fetch(url)
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                // Obtenha as coordenadas geográficas do primeiro resultado
                var lon = parseFloat(data[0].lon);
                var lat = parseFloat(data[0].lat);

                // Crie um marcador e adicione-o ao mapa
                var marker = new ol.Feature({
                    geometry: new ol.geom.Point(ol.proj.fromLonLat([lon, lat]))
                });

                var vectorSource = new ol.source.Vector({
                    features: [marker]
                });

                var vectorLayer = new ol.layer.Vector({
                    source: vectorSource
                });

                map.addLayer(vectorLayer);

                // Ajuste o centro do mapa para o marcador
                map.getView().setCenter(ol.proj.fromLonLat([lon, lat]));
                map.getView().setZoom(14);
            } else {
                console.error('Nenhum resultado de geocodificação encontrado.');
            }
        })
        .catch(error => {
            console.error('Erro ao obter resultados de geocodificação:', error);
        });
}

function CityPerimeter(data, city) {
    
    var cityCoordinates = [data];
    // Use as coordenadas do centro da cidade e ajuste o nível de zoom
    var map = L.map("map").setView([c_lon, c_lat], 12); 
    L.tileLayer(city, {
        attribution: "© OpenStreetMap contributors",
    }).addTo(map);
    L.polygon(cityCoordinates, { color: "gray" }).addTo(map);
}

function IniciarMapa(cidade) {
    geocodeCityAndInitializeMap(map, cidade);
};
function AddMarker(endereco) {
    geocodeAndAddMarker(map, endereco);
};

function GetLocais(data) {
    for (const item of data) {
        AddMarker(item);
    }
};
