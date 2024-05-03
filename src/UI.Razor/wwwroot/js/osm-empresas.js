// Função para estilizar as áreas GeoJSON com base no valor da propriedade "empresas"
function style(feature) {
    return {
        fillColor: getColor(feature.properties.empresas),
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}

function zoomToFeature(e) {
    //map.fitBounds(e.target.getBounds());        
    var layer = e.target;
    const url = '/maps/' + layer.feature.properties.geocode;
    // Redireciona para a página interna
    location.href = url;
}

