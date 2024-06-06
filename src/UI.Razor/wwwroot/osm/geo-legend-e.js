// legendas
var legend = L.control({ position: 'bottomright' });

legend.onAdd = function (map) {

    var div = L.DomUtil.create('div', 'info legend'),
        grades = [0, 5000, 10000, 15000, 20000, 25000, 48000, 80000, 160000, 320000],
        labels = [];

    div.innerHTML += 'Empresas<div class="divider"></div>'
    // loop through our density intervals and generate a label with a colored square for each interval
    for (var i = 0; i < grades.length; i++) {
        div.innerHTML +=
            '<i style="background:' + getColor(grades[i] + 1) + '"></i> ' +
            grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '<br>' : '+');
    }

    return div;
};

//cores padrões
function getColor(d) {
    return d > 320000 ? '#34000f' :
        d > 160000 ? '#4d0017' :
            d > 80000 ? '#800026' :
                d > 40000 ? '#BD0026' :
                    d > 25000 ? '#E31A1C' :
                        d > 20000 ? '#FC4E2A' :
                            d > 15000 ? '#FD8D3C' :
                                d > 10000 ? '#FEB24C' :
                                    d > 5000 ? '#FED976' :
                                        '#FFEDA0';
}
