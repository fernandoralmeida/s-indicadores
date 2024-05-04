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