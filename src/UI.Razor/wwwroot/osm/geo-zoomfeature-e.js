function zoomToFeature(e) {
    //map.fitBounds(e.target.getBounds());        
    var layer = e.target;
    $("#loader").show();
    $(".blackboxload").show();
    const url = '/maps/' + layer.feature.properties.geocode;
    // Redireciona para a página interna
    location.href = url;
}

