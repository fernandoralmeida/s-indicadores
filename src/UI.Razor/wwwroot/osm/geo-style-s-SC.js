function styleSC(feature) {
    var _color = getColorSC(`${feature.properties.empresas}`);
    return {
        fillColor: _color,
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}