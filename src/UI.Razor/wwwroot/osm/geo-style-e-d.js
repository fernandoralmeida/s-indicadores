// Função para estilizar as áreas GeoJSON com base no valor da propriedade "empresas"
function style(feature) {
    var _color = getColor(feature.properties.empresas);
    return {
        fillColor: _color,
        weight: 3,
        opacity: 1,
        color: 'black',
        //dashArray: '3',
        fillOpacity: 0
    };
}