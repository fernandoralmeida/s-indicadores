
function renderCnae(data) {
    const cnaeContainer = document.getElementById("cnae-container");
    cnaeContainer.innerHTML = ""; // Limpa o conteúdo existente

    data.forEach(setor => {
        const setorItem = document.createElement("li");
        const setorHeader = document.createElement("div");
        setorHeader.classList.add("collapsible-header");
        setorHeader.innerHTML = `<i class="material-icons">place</i>${setor.Key}`;
        setorItem.appendChild(setorHeader);

        const setorBody = document.createElement("div");
        setorBody.classList.add("collapsible-body", "padding-10");
        setorItem.appendChild(setorBody);

        const segmentoList = document.createElement("ul");
        segmentoList.classList.add("collapsible", "margin-0");
        setorBody.appendChild(segmentoList);

        setor.Value.forEach(segmento => {
            const segmentoItem = document.createElement("li");
            segmentoList.appendChild(segmentoItem);

            const segmentoHeader = document.createElement("div");
            segmentoHeader.classList.add("collapsible-header");
            segmentoHeader.title = `CNAE Grupo: ${segmento.Key.substr(0, 2)}`;
            segmentoHeader.textContent = segmento.Key.substr(2);
            segmentoItem.appendChild(segmentoHeader);

            const segmentoBody = document.createElement("div");
            segmentoBody.classList.add("collapsible-body", "padding-10");
            segmentoItem.appendChild(segmentoBody);

            const segmentoLink = document.createElement("a");
            segmentoLink.classList.add("segmento_click", "btn-link");
            segmentoLink.setAttribute("data-segmento", segmento.Key.substr(0, 2));
            segmentoLink.setAttribute("data-descricao", segmento.Key.substr(2));
            segmentoLink.title = `CNAE Grupo: ${segmento.Key.substr(0, 2)}`;
            segmentoLink.textContent = `VER SEGMENTO ${segmento.Key.substr(2)}`;
            segmentoBody.appendChild(segmentoLink);

            segmento.Value.forEach(cnae => {
                const cnaeParagraph = document.createElement("p");
                segmentoBody.appendChild(cnaeParagraph);

                const cnaeLink = document.createElement("a");
                cnaeLink.classList.add("cnae_click");
                cnaeLink.setAttribute("data-cnae", cnae.substr(0, 7));
                cnaeLink.setAttribute("data-descricao", cnae.substr(7));
                cnaeLink.title = `CNAE ${cnae.substr(0, 7)}`;
                cnaeLink.textContent = cnae.substr(7);
                cnaeParagraph.appendChild(cnaeLink);

                const divider = document.createElement("div");
                divider.classList.add("divider");
                segmentoBody.appendChild(divider);
            });
        });

        cnaeContainer.appendChild(setorItem);
    });
}