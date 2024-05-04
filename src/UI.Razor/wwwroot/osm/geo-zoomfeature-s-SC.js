function zoomToFeatureSC(e) {
    var layer = e.target;
    $("#loader").show();
    $(".blackboxload").show();
    const url = '/setores/' + _page_link + '/' + layer.feature.properties.geocode;
    // Redireciona para a página interna
    location.href = url;
}