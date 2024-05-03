//ignora o IndexDB e força busca direto na API
function doMapByCNAE(_url) {
    $("#loader").show();
    $(".blackboxload").show();
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
            $("#loader").hide();
            $(".blackboxload").fadeOut("slow");
        })
        .catch(error => console.error('Erro ao carregar dados GeoJSON da API:', error));
}

function styleSC(feature) {
    return {
        fillColor: getColorSC(`${feature.properties.empresas}`),
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}

function zoomToFeatureSC(e) {
    var layer = e.target;
    $("#loader").show();
    $(".blackboxload").show();
    const url = '/setores/' + _page_link + '/' + layer.feature.properties.geocode;
    // Redireciona para a página interna
    location.href = url;
}

function onEachFeatureSC(feature, layer) {
    layer.on({
        mouseover: highlightFeature,
        mouseout: resetHighlight,
        click: zoomToFeatureSC
    });
}

