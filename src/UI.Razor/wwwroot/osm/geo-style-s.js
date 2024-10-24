function style(feature) {
    var _color = getColor(`${feature.properties.setor}`);
    return {
        fillColor: _color,
        weight: 1,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0.7
    };
}