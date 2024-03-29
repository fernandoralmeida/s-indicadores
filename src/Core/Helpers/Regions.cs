namespace IDN.Core.Helpers;

public class Regions
{
    public static IEnumerable<string> MacroRegioesSP()
    {
        return new List<string>()
        {
            "Araçatuba",
            "Presidente Prudente",
            "São José do Rio Preto",
            "Barretos",
            "Franca",
            "Marília",
            "Bauru",
            "Central",
            "Ribeirão Preto",
            "Itapeva",
            "Sorocaba",
            "Campinas",
            "Registro",
            "Santos",
            "São Paulo",
            "São José dos Campos"
        }.OrderBy(s => s);
    }

    public static IEnumerable<string> RegioesGovernoSP() =>
    new List<string>() {
        "Presidente Prudente", "Dracena","Andradina","Jales","Fernandopolis","Votuporanga","Araçatuba","Adamantina",
        "Assis","Tupã","Lins","São José do Rio Preto","Barretos","Catanduva","Marília","Ourinhos","Bauru","Araraquara",
        "Ribeirão Preto","São Joaquim da Barra","Franca","Avaré","Botucatu","Jaú","São Carlos","São João da Boa Vista",
        "Limeira","Rio Claro","Piracicaba","Campinas","Itapetininga","Itapeva","Bragança Paulista","Jundiaí",
        "Sorocaba","Registro","Santos","São Paulo","São José dos Campos","Taubaté","Caraguatatuba","Guaratinguetá",
        "Caraguatatuba","Cruzeiro"
     }.OrderBy(s => s);

    public static readonly Dictionary<string, IEnumerable<string>> MacroRegoesRASP = new()
    {
        {"presidente-prudente", MacroRegiaoPresidentePrudenteSP()},
        {"aracatuba", MacroRegiaoAracatubaSP()},
        {"sao-jose-do-rio-preto", MacroRegiaoSaoJoseRioPretoSP()},
        {"barretos", MacroRegiaoBarretosSP()},
        {"franca", MacroRegiaoFrancaSP()},
        {"ribeirao-preto", MacroRegiaoRibeiraoPretoSP()},
        {"central", MacroRegiaoCentalSP()},
        {"bauru", MacroRegiaoBauruSP()},
        {"marilia", MacroRegiaoMariliaSP()},
        {"itapeva", MacroRegiaoItapevaSP()},
        {"sorocaba", MacroRegiaoSorocabaSP()},
        {"campinas", MacroRegiaoCampinasSP()},
        {"sao-jose-dos-campos", MacroRegiaoSaoJosedosCampos()},
        {"santos", MacroRegiaoSantosSP()},
        {"sao-paulo", MacroRegiaoSaoPauloSP()},
        {"registro", MacroRegiaoRegistroSP()}
    };

    private static IEnumerable<string> MacroRegiaoBauruSP() =>
        new List<string>() {
            "Agudos",
            "Arealva",
            "Avai",
            "Balbinos",
            "Bariri",
            "Barra Bonita",
            "Bauru",
            "Bocaina",
            "Boraceia",
            "Borebi",
            "Cabralia Paulista",
            "Cafelandia",
            "Dois Corregos",
            "Duartina",
            "Getulina",
            "Guaicara",
            "Guaimbe",
            "Guaranta",
            "Iacanga",
            "Igaracu do Tiete",
            "Itaju",
            "Itapui",
            "Jau",
            "Lencois Paulista",
            "Lins",
            "Lucianopolis",
            "Macatuba",
            "Mineiros do Tiete",
            "Paulistania",
            "Pederneiras",
            "Pirajui",
            "Piratininga",
            "Pongai",
            "Presidente Alves",
            "Promissao",
            "Reginopolis",
            "Sabino",
            "Ubirajara",
            "Uru"}.OrderBy(s => s);

    private static IEnumerable<string> MacroRegiaoPresidentePrudenteSP() =>
    new List<string>() {
        "Adamantina",
        "Alfredo Marcondes",
        "Alvares Machado",
        "Anhumas",
        "Caiabu",
        "Caiua",
        "Dracena",
        "Emilianopolis",
        "Estrela do Norte",
        "Euclides da Cunha Paulista",
        "Flora Rica",
        "Florida Paulista",
        "Iepe",
        "Indiana",
        "Inubia Paulista",
        "Irapuru",
        "Junqueiropolis",
        "Lucelia",
        "Maraba Paulista",
        "Mariapolis",
        "Martinopolis",
        "Mirante do Paranapanema",
        "Monte Castelo",
        "Nantes",
        "Narandiba",
        "Nova Guataporanga",
        "Osvaldo Cruz",
        "Ouro Verde",
        "Pacaembu",
        "Panorama",
        "Pauliceia",
        "Piquerobi",
        "Pirapozinho",
        "Pracinha",
        "Presidente Bernardes",
        "Presidente Epitacio",
        "Presidente Prudente",
        "Presidente Venceslau",
        "Rancharia",
        "Regente Feijo",
        "Ribeirao dos Indios",
        "Rosana",
        "Sagres",
        "Salmourao",
        "Sandovalina",
        "Santa Mercedes",
        "Santo Anastacio",
        "Santo Expedito",
        "Sao Joao do Pau d'Alho",
        "Taciba",
        "Tarabai",
        "Teodoro Sampaio",
        "Tupi Paulista"
    };

    private static IEnumerable<string> MacroRegiaoAracatubaSP() =>
    new List<string>(){
        "Alto Alegre",
        "Andradina",
        "Araçatuba",
        "Auriflama",
        "Avanhandava",
        "Barbosa",
        "Bento de Abreu",
        "Bilac",
        "Birigüi",
        "Braúna",
        "Brejo Alegre",
        "Buritama",
        "Castilho",
        "Clementina",
        "Coroados",
        "Gabriel Monteiro",
        "Gastão Vidigal",
        "General Salgado",
        "Glicério",
        "Guaraçaí",
        "Guararapes",
        "Guzolândia",
        "Ilha Solteira",
        "Itapura",
        "Lavínia",
        "Lourdes",
        "Luiziânia",
        "Mirandópolis",
        "Murutinga do Sul",
        "Nova Castilho",
        "Nova Independência",
        "Nova Luzitânia",
        "Penápolis",
        "Pereira Barreto",
        "Piacatu",
        "Rubiácea",
        "Santo Antonio do Aracanguá",
        "Santópolis do Aguapeí",
        "São João de Iracema",
        "Sud Mennucci",
        "Suzanápolis",
        "Turiúba",
        "Valparaíso"
    };

    private static IEnumerable<string> MacroRegiaoSaoJoseRioPretoSP() =>
    new List<string>(){
        "Adolfo",
        "Álvares Florence",
        "Américo de Campos",
        "Aparecida d'Oeste",
        "Ariranha",
        "Aspásia",
        "Bady Bassitt",
        "Bálsamo",
        "Cardoso",
        "Catanduva",
        "Catiguá",
        "Cedral",
        "Cosmorama",
        "Dirce Reis",
        "Dolcinópolis",
        "Elisiário",
        "Estrela d'Oeste",
        "Fernandópolis",
        "Floreal",
        "Guapiaçu",
        "Guarani d'Oeste",
        "Ibirá",
        "Icém",
        "Indiaporã",
        "Ipiguá",
        "Irapuã",
        "Itajobi",
        "Jaci",
        "Jales",
        "José Bonifácio",
        "Macaubal",
        "Macedônia",
        "Magda",
        "Marapoama",
        "Marinópolis",
        "Mendonça",
        "Meridiano",
        "Mesópolis",
        "Mira Estrela",
        "Mirassol",
        "Mirassolândia",
        "Monções",
        "Monte Aprazível",
        "Neves Paulista",
        "Nhandeara",
        "Nipoã",
        "Nova Aliança",
        "Nova Canaã Paulista",
        "Nova Granada",
        "Novais",
        "Novo Horizonte",
        "Onda Verde",
        "Orindiúva",
        "Ouroeste",
        "Palestina",
        "Palmares Paulista",
        "Palmeira d'Oeste",
        "Paraíso",
        "Paranapuã",
        "Parisi",
        "Paulo de Faria",
        "Pedranópolis",
        "Pindorama",
        "Planalto",
        "Poloni",
        "Pontalinda",
        "Pontes Gestal",
        "Populina",
        "Potirendaba",
        "Riolândia",
        "Rubinéia",
        "Sales",
        "Santa Adélia",
        "Santa Albertina",
        "Santa Clara d'Oeste",
        "Santa Fé do Sul",
        "Santa Rita d'Oeste",
        "Santa Salete",
        "Santana da Ponte Pensa",
        "São Francisco",
        "São João das Duas Pontes",
        "São José do Rio Preto",
        "Sebastianópolis do Sul",
        "Tabapuã",
        "Tanabi",
        "Três Fronteiras",
        "Turmalina",
        "Ubarana",
        "Uchôa",
        "União Paulista",
        "Urânia",
        "Urupês",
        "Valentim Gentil",
        "Vitória Brasil",
        "Votuporanga",
        "Zacarias"
    };

    private static IEnumerable<string> MacroRegiaoBarretosSP() =>
    new List<string>(){
        "Altair",
        "Barretos",
        "Bebedouro",
        "Cajobi",
        "Colina",
        "Colômbia",
        "Embaúba",
        "Guaíra",
        "Guaraci",
        "Jaborandi",
        "Monte Azul Paulista",
        "Olímpia",
        "Pirangi",
        "Severínia",
        "Taiaçu",
        "Taiúva",
        "Terra Roxa",
        "Viradouro",
        "Vista Alegre do Alto"
    };

    private static IEnumerable<string> MacroRegiaoFrancaSP() =>
    new List<string>(){
        "Aramina",
        "Batatais",
        "Buritizal",
        "Cristais Paulista",
        "Franca",
        "Guará",
        "Igarapava",
        "Ipuã",
        "Itirapuã",
        "Ituverava",
        "Jeriquara",
        "Miguelópolis",
        "Morro Agudo",
        "Nuporanga",
        "Orlândia",
        "Patrocínio Paulista",
        "Pedregulho",
        "Restinga",
        "Ribeirão Corrente",
        "Rifaina",
        "Sales Oliveira",
        "São Joaquim da Barra",
        "São José da Bela Vista"
    };

    private static IEnumerable<string> MacroRegiaoRibeiraoPretoSP() =>
    new List<string>(){
        "Altinópolis",
        "Barrinha",
        "Brodowski",
        "Cajuru",
        "Cássia dos Coqueiros",
        "Cravinhos",
        "Dumont",
        "Guariba",
        "Guatapará",
        "Jaboticabal",
        "Jardinópolis",
        "Luís Antônio",
        "Monte Alto",
        "Pitangueiras",
        "Pontal",
        "Pradópolis",
        "Ribeirão Preto",
        "Santa Cruz da Esperança",
        "Santa Rosa de Viterbo",
        "Santo Antonio da Alegria",
        "São Simão",
        "Serra Azul",
        "Serrana",
        "Sertãozinho",
        "Taquaral"
    };

    private static IEnumerable<string> MacroRegiaoCentalSP() =>
    new List<string>(){
        "Américo Brasiliense",
        "Araraquara",
        "Boa Esperança do Sul",
        "Borborema",
        "Cândido Rodrigues",
        "Descalvado",
        "Dobrada",
        "Dourado",
        "Fernando Prestes",
        "Gavião Peixoto",
        "Ibaté",
        "Ibitinga",
        "Itápolis",
        "Matão",
        "Motuca",
        "Nova Europa",
        "Porto Ferreira",
        "Ribeirão Bonito",
        "Rincão",
        "Santa Ernestina",
        "Santa Lúcia",
        "Santa Rita do Passa Quatro",
        "São Carlos",
        "Tabatinga",
        "Taquaritinga",
        "Trabiju"
};

    private static IEnumerable<string> MacroRegiaoMariliaSP() =>
    new List<string>(){
        "Álvaro de Carvalho",
        "Alvinlândia",
        "Arco-Íris",
        "Assis",
        "Bastos",
        "Bernardino de Campos",
        "Borá",
        "Campos Novos Paulista",
        "Cândido Mota",
        "Canitar",
        "Chavantes",
        "Cruzália",
        "Echaporã",
        "Espírito Santo do Turvo",
        "Fernão",
        "Florínea",
        "Gália",
        "Garça",
        "Herculândia",
        "Iacri",
        "Ibirarema",
        "Ipaussu",
        "João Ramalho",
        "Júlio Mesquita",
        "Lupércio",
        "Lutécia",
        "Maracaí",
        "Marília",
        "Ocauçu",
        "Óleo",
        "Oriente",
        "Oscar Bressane",
        "Ourinhos",
        "Palmital",
        "Paraguaçu Paulista",
        "Parapuã",
        "Pedrinhas Paulista",
        "Platina",
        "Pompéia",
        "Quatá",
        "Queiroz",
        "Quintana",
        "Ribeirão do Sul",
        "Rinópolis",
        "Salto Grande",
        "Santa Cruz do Rio Pardo",
        "São Pedro do Turvo",
        "Tarumã",
        "Timburi",
        "Tupã",
        "Vera Cruz"
    };

    private static IEnumerable<string> MacroRegiaoCampinasSP() =>
    new List<string>(){
        "Aguaí",
        "Águas da Prata",
        "Águas de Lindóia",
        "Águas de São Pedro",
        "Americana",
        "Amparo",
        "Analândia",
        "Araras",
        "Artur Nogueira",
        "Atibaia",
        "Bom Jesus dos Perdões",
        "Bragança Paulista",
        "Brotas",
        "Cabreúva",
        "Caconde",
        "Campinas",
        "Campo Limpo Paulista",
        "Capivari",
        "Casa Branca",
        "Charqueada",
        "Conchal",
        "Cordeirópolis",
        "Corumbataí",
        "Cosmópolis",
        "Divinolândia",
        "Elias Fausto",
        "Engenheiro Coelho",
        "Espírito Santo do Pinhal",
        "Estiva Gerbi",
        "Holambra",
        "Hortolândia",
        "Indaiatuba",
        "Ipeúna",
        "Iracemápolis",
        "Itapira",
        "Itatiba",
        "Itirapina",
        "Itobi",
        "Itupeva",
        "Jaguariúna",
        "Jarinu",
        "Joanópolis",
        "Jundiaí",
        "Leme",
        "Limeira",
        "Lindóia",
        "Louveira",
        "Mococa",
        "Mogi-Guaçu",
        "Mogi Mirim",
        "Mombuca",
        "Monte Alegre do Sul",
        "Monte Mor",
        "Morungaba",
        "Nazaré Paulista",
        "Nova Odessa",
        "Paulínia",
        "Pedra Bela",
        "Pedreira",
        "Pinhalzinho",
        "Piracaia",
        "Piracicaba",
        "Pirassununga",
        "Rafard",
        "Rio Claro",
        "Rio das Pedras",
        "Saltinho",
        "Santa Bárbara d'Oeste",
        "Santa Cruz da Conceição",
        "Santa Cruz das Palmeiras",
        "Santa Gertrudes",
        "Santa Maria da Serra",
        "Santo Antonio de Posse",
        "Santo Antonio do Jardim",
        "São João da Boa Vista",
        "São José do Rio Pardo",
        "São Pedro",
        "São Sebastião da Grama",
        "Serra Negra",
        "Socorro",
        "Sumaré",
        "Tambaú",
        "Tapiratiba",
        "Torrinha",
        "Tuiuti",
        "Valinhos",
        "Vargem",
        "Vargem Grande do Sul",
        "Várzea Paulista",
        "Vinhedo"
    };

    private static IEnumerable<string> MacroRegiaoSorocabaSP() =>
    new List<string>(){
        "Águas de Santa Bárbara",
        "Alambari",
        "Alumínio",
        "Anhembi",
        "Araçariguama",
        "Araçoiaba da Serra",
        "Areiópolis",
        "Avaré",
        "Bofete",
        "Boituva",
        "Botucatu",
        "Cabreuva",
        "Capela do Alto",
        "Cerqueira Cesar",
        "Cerquilho",
        "Cesário Lange",
        "Conchas",
        "Guareí",
        "Iaras",
        "Ibiúna",
        "Iperó",
        "Itapetininga",
        "Itatinga",
        "Itu",
        "Jumirim",
        "Laranjal Paulista",
        "Mairinque",
        "Pardinho",
        "Pereiras",
        "Piedade",
        "Pilar do Sul",
        "Porangaba",
        "Porto Feliz",
        "Pratânia",
        "Quadra",
        "Salto",
        "Salto de Pirapora",
        "São Manuel",
        "São Miguel Arcanjo",
        "São Roque",
        "Sarapuí",
        "Sorocaba",
        "Tapiraí",
        "Tatuí",
        "Tietê",
        "Torre de Pedra",
        "Votorantim"
    };

    private static IEnumerable<string> MacroRegiaoItapevaSP() =>
    new List<string>(){
        "Angatuba",
        "Apiaí",
        "Arandu",
        "Barão de Antonina",
        "Barra do Chapéu",
        "Bom Sucesso de Itararé",
        "Buri",
        "Campina do Monte Alegre",
        "Capão Bonito",
        "Coronel Macedo",
        "Fartura",
        "Guapiara",
        "Iporanga",
        "Itaberá",
        "Itaí",
        "Itaóca",
        "Itapeva",
        "Itapirapuã Paulista",
        "Itaporanga",
        "Itararé",
        "Nova Campina",
        "Paranapanema",
        "Piraju",
        "Ribeira",
        "Ribeirão Branco",
        "Ribeirão Grande",
        "Riversul",
        "Sarutaiá",
        "Taguaí",
        "Taquarituba",
        "Taquarivaí",
        "Tejupá"
    };

    private static IEnumerable<string> MacroRegiaoRegistroSP() =>
    new List<string>(){
        "Barra do Turvo",
        "Cajati",
        "Cananéia",
        "Eldorado",
        "Iguape",
        "Ilha Comprida",
        "Itariri",
        "Jacupiranga",
        "Juquiá",
        "Miracatu",
        "Pariquera-Açu",
        "Pedro de Toledo",
        "Registro",
        "Sete Barras"
    };

    private static IEnumerable<string> MacroRegiaoSaoPauloSP() =>
    new List<string>() {
        "Arujá",
        "Barueri",
        "Biritiba-Mirim",
        "Caieiras",
        "Cajamar",
        "Carapicuíba",
        "Cotia",
        "Diadema",
        "Embu das Artes",
        "Embu-Guaçu",
        "Ferraz de Vasconcelos",
        "Francisco Morato",
        "Franco da Rocha",
        "Guararema",
        "Guarulhos",
        "Itapecerica da Serra",
        "Itapevi",
        "Itaquaquecetuba",
        "Jandira",
        "Juquitiba",
        "Mairiporã",
        "Mauá",
        "Mogi das Cruzes",
        "Osasco",
        "Pirapora do Bom Jesus",
        "Poá",
        "Ribeirão Pires",
        "Rio Grande da Serra",
        "Salesópolis",
        "Santa Isabel",
        "Santana de Parnaíba",
        "Santo André",
        "São Bernardo do Campo",
        "São Caetano do Sul",
        "São Lourenço da Serra",
        "São Paulo",
        "Suzano",
        "Taboão da Serra",
        "Vargem Grande Paulista"
    };

    private static IEnumerable<string> MacroRegiaoSantosSP() =>
    new List<string>()
    {
        "Bertioga",
        "Cubatão",
        "Guarujá",
        "Itanhaém",
        "Mongaguá",
        "Peruíbe",
        "Praia Grande",
        "Santos",
        "São Vicente"
    };

    private static IEnumerable<string> MacroRegiaoSaoJosedosCampos() =>
    new List<string>() {
        "Aparecida",
        "Arapeí",
        "Areias",
        "Bananal",
        "Caçapava",
        "Cachoeira Paulista",
        "Campos do Jordão",
        "Canas",
        "Caraguatatuba",
        "Cruzeiro",
        "Cunha",
        "Guaratinguetá",
        "Igaratá",
        "Ilhabela",
        "Jacareí",
        "Jambeiro",
        "Lagoinha",
        "Lavrinhas",
        "Lorena",
        "Monteiro Lobato",
        "Natividade da Serra",
        "Paraibuna",
        "Pindamonhangaba",
        "Piquete",
        "Potim",
        "Queluz",
        "Redenção da Serra",
        "Roseira",
        "Santa Branca",
        "Santo Antonio do Pinhal",
        "São Bento do Sapucaí",
        "São José do Barreiro",
        "São José dos Campos",
        "São Luíz do Paraitinga",
        "São Sebastião",
        "Silveiras",
        "Taubaté",
        "Tremembé",
        "Ubatuba"
    };

}