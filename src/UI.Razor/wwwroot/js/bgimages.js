// Declara um array de imagens
const imagens = [
"_8bfab692-0bcc-438e-b6d7-2c13d0d6fb5c.jpg",
"_87c1ab20-efd4-4811-93bf-742210bb88d4.jpg",
"_665b1f4d-1c1c-40ff-8c70-34b38871c9c0.jpg",
"_a2ad69b4-a9cc-46ab-83dc-089d09d4d5fc.jpg",
"_a474f7ca-2496-4ada-950d-72aa0a64f353.jpg",
"_d1b738cb-e78b-45d8-b4aa-aea1c763067b.jpg",
"_d33e9fea-fa14-4a44-9634-612e92b53e28.jpg",
"_f4158fad-ebff-4023-a977-746e84c10e67.jpg"];

// Escolhe uma imagem aleatória do array
function escolherImagemAleatoria() {
    const indiceImagem = Math.floor(Math.random() * imagens.length);
    return imagens[indiceImagem];
}

// Carrega a imagem aleatória
function carregarImagem() {
    const imagem = escolherImagemAleatoria();
    document.querySelector(".bg-image").style.backgroundImage = `url(../images/${imagem})`;
}

// Carrega a imagem aleatória quando a página é carregada
//window.onload = carregarImagem;