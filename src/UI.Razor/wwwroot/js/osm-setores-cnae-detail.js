function doMapByDetail(_url) {
    // Carregar dados GeoJSON de uma API
    fetch(_url)
        .then(response => response.json())
        .then(data => {

            $("#loader").hide();
            $(".blackboxload").fadeOut("slow");

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

function styleSC(feature) {
    return {
        fillColor: getColorSC(`${_max}`),
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}

function zoomToFeatureSC(e) {
    map.fitBounds(e.target.getBounds());
}

function onEachFeatureSC(feature, layer) {
    layer.on({
        mouseover: highlightFeature,
        mouseout: resetHighlight,
        click: zoomToFeatureSC
    });
}