// Legenda Segmento e Cnae
var legend = L.control({ position: 'bottomright' });

legend.onAdd = function (map) {

    var div = L.DomUtil.create('div', 'info legend'),
        grades = ["Industria", "Comercio", "Servicos", "Construcao", "Agro"],
        labels = [];

    div.innerHTML += 'Setores<div class="divider"></div>'
    // loop through our density intervals and generate a label with a colored square for each interval
    for (var i = 0; i < grades.length; i++) {
        div.innerHTML +=
            '<i style="background:' + getColor(grades[i] + 1) + '"></i> ' +
            grades[i] + '<br>';
    }

    return div;
};

//cores para os setores
function getColor(d) {
    const prefixo = d.substr(0, 3);
    switch (prefixo) {
        case 'Ser':
            return '#4797FF';
        case 'Com':
            return '#ffa500';
        case 'Ind':
            return '#8a2be2';
        case 'Con':
            return '#d87093';
        case 'Agr':
            return '#00E6C8';
        default:
            return '#FFEDA0';
    }
}