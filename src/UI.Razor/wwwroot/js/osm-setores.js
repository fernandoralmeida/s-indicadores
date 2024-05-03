// Somente para Segmento e Cnae
function style(feature) {
    return {
        fillColor: getColor(`${feature.properties.setor}`),
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}

// Zoom para setores
function zoomToFeature(e) {
    map.fitBounds(e.target.getBounds());
}
