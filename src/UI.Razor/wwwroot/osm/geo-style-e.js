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