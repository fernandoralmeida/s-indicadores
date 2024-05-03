$(document).ready(function () {
    $('.collapsible').collapsible();
    $('.sidenav').sidenav();
    const spanResult = document.getElementById("span_result");

    $(".segmento_click").click(function () {
        const p = this.dataset.segmento;
        const desc =`* ${this.dataset.descricao} *`;
        _page_link = p;
        doMapByCNAE(`/api/v1/geojson/segmento/` + p);                
        spanResult.textContent = desc;
    });
    $(".cnae_click").click(function () {
        const p = this.dataset.cnae;
        const desc =`* ${this.dataset.descricao} *`;
        _page_link = p;
        doMapByCNAE(`/api/v1/geojson/cnae/` + p);
        spanResult.textContent = desc; 
    });
    $(".rag_click").click(function () {
        const p = this.dataset.macro;
        const e = this.dataset.estado;
        _page_link = p;
        const url = `/maps/${e}/${p}`;         
        location.href = url;
    });

    $(".submit").click(function () {
        $(".blackboxload").show();
        $("#loader").show();
        $(this).addClass('pre-active');
    });
});