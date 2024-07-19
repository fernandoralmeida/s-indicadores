//ignora o IndexDB e força busca direto na API
//usado para funcoes SC
function doMapByCNAE(_url) {
    //$("#loader").show();
    //$(".blackboxload").show();

    var loadingMapDiv = document.getElementById('loadingMap');
    // Tornar a div visível alterando a propriedade display
    loadingMapDiv.classList.add("visible-on");

    // Limpar o mapa (opcional)
    map.eachLayer(function (layer) {
        if (layer instanceof L.GeoJSON) {
            map.removeLayer(layer);
        }
    });

    // 2. Remover controles existentes
    map.removeControl(legend);

    //get from url api
    fetch(_url)
        .then(response => response.json())
        .then(data => {

            // Processar os dados GeoJSON

            _max = data.max;
            _min = data.min;

            // Adicionar camada GeoJSON ao mapa
            var geojsonLayer = L.geoJSON(data, {
                style: styleSC,
                onEachFeature: onEachFeatureSC
            }).addTo(map);

            legendSC.addTo(map);

            // Ajustar o mapa para abranger a área GeoJSON com zoom
            map.fitBounds(geojsonLayer.getBounds());
        })
        .catch(error => console.error('Erro ao carregar dados GeoJSON da API:', error))
        .finally(() => {
            // Código a ser executado sempre, independentemente do sucesso ou falha da requisição
            loadingMapDiv.classList.remove("visible-on");
            loadingMapDiv.classList.add("visible-off");;
        });
}

//usado para funcoes SC
function doMapByDetail(_url) {

    var loadingMapDiv = document.getElementById('loadingMap');
    // Tornar a div visível alterando a propriedade display
    loadingMapDiv.classList.add("visible-on");
    // Carregar dados GeoJSON de uma API
    fetch(_url)
        .then(response => response.json())
        .then(data => {

            // $("#loader").hide();
            // $(".blackboxload").fadeOut("slow");

            // Adicionar camada GeoJSON ao mapa
            var geojsonLayer = L.geoJSON(data, {
                style: styleSC,
                onEachFeature: onEachFeatureSC
            }).addTo(map);

            legendSC.addTo(map);

            // Ajustar o mapa para abranger a área GeoJSON com zoom
            map.fitBounds(geojsonLayer.getBounds());
        })
        .catch(error => console.error('Erro ao carregar dados GeoJSON:', error))
        .finally(() => {
            // Código a ser executado sempre, independentemente do sucesso ou falha da requisição
            loadingMapDiv.classList.remove("visible-on");
            loadingMapDiv.classList.add("visible-off");;
        });
}
