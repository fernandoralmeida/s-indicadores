//ignora o IndexDB e força busca direto na API
//usado para funcoes SC
function doMapByCNAE(_url) {
    //$("#loader").show();
    //$(".blackboxload").show();
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
            // $("#loader").hide();
            // $(".blackboxload").fadeOut("slow");
        })
        .catch(error => console.error('Erro ao carregar dados GeoJSON da API:', error));
}

//usado para funcoes SC
function doMapByDetail(_url) {
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
        .catch(error => console.error('Erro ao carregar dados GeoJSON:', error));
}
