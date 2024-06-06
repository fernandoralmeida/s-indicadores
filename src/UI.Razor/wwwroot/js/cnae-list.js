const setoresContainer = document.getElementById('setores');

function carregarSetores(urlAPI) {
    setoresContainer.innerHTML = '...'; 
    fetch(urlAPI)
        .then(response => response.json())
        .then(data => {
            renderSetores(data);
        })
        .catch(error => console.error('Erro ao carregar setores:', error));
}

function renderSetores(data) {
    setoresContainer.innerHTML = ''; // Limpa o conteúdo anterior
    data.forEach(setor => {
        const _li = document.createElement('div');

        // const collapsibleHeader = document.createElement('div');
        // collapsibleHeader.classList.add('collapsible-header');
        // collapsibleHeader.textContent = setor.key;
        // _li.appendChild(collapsibleHeader);

        const collapsibleBody = document.createElement('div');
        collapsibleBody.classList.add('padding-5');

        setor.value.forEach(segmento => {

            const _divLink = document.createElement('div');
            _divLink.classList.add('row', 'line-normal', 'padding-t-10', 'margin-b-10');

            const _aLink = document.createElement('a');
            _aLink.classList.add('btn-small', 'sidenav-close');
            _aLink.href = '/setores/' + segmento.key.slice(0, 2);
            _aLink.dataset.segmento = segmento.key.slice(0, 2);
            _aLink.dataset.descricao = segmento.key.slice(2);
            _aLink.textContent = `> ${segmento.key.slice(2)}`;
            _aLink.title = `CNAE Grupo: ${segmento.key.slice(0, 2)}`;
            _aLink.addEventListener("click", function () {
                // Prevenir o comportamento padrão do link
                //event.preventDefault();
                submitClick();
            });

            _divLink.appendChild(_aLink);
            collapsibleBody.appendChild(_divLink);

            segmento.value.forEach(cnae => {
                const _p = document.createElement('p');
                const _a = document.createElement('a');
                _a.classList.add('sidenav-close', 'truncate');
                _a.href = '/setores/' + cnae.slice(0, 7);
                _a.dataset.cnae = cnae.slice(0, 7);
                _a.dataset.descricao = cnae.slice(7);
                _a.textContent = `>> ${cnae.slice(7)}`;
                _a.title = `CNAE ${cnae.slice(0, 7)}`;                
                _a.addEventListener("click", function () {
                    submitClick();
                });

                _p.appendChild(_a);
                collapsibleBody.appendChild(_p);
            });

            const _divider = document.createElement('div');
            _divider.classList.add('divider');
            collapsibleBody.appendChild(_divider);
        });
        _li.appendChild(collapsibleBody);
        setoresContainer.appendChild(_li);
    })
}