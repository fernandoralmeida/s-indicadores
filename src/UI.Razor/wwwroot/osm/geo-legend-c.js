// legendas
var legendSC = L.control({ position: 'bottomleft' });

legendSC.onAdd = function (map) {

    var div = L.DomUtil.create('div', 'info legend'),
        grades = [1, 10, 30, 90, 270, 810, 2430, 7290, 21870, 65000],
        labels = [];

    div.innerHTML += 'Empresas<div class="divider"></div>'
    // loop through our density intervals and generate a label with a colored square for each interval
    for (var i = 0; i < grades.length; i++) {
        div.innerHTML +=
            '<i style="background:' + getColorSC(grades[i] + 1) + '"></i> ' +
            grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '<br>' : '+');
    }

    return div;
};

function getColorSC(d) {
    return d > 65000 ? '#34000f' :
        d > 21870 ? '#4d0017' :
            d > 7290 ? '#800026' :
                d > 2430 ? '#BD0026' :
                    d > 810 ? '#E31A1C' :
                        d > 270 ? '#FC4E2A' :
                            d > 90 ? '#FD8D3C' :
                                d > 30 ? '#FEB24C' :
                                    d > 10 ? '#FED976' :
                                        '#FFEDA0';
}