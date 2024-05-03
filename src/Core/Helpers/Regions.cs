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

    public static IEnumerable<string> RegioesMetropolitanasSP() => new List<string>()
    {
        "Campinas", "São Paulo","Baixada Santista","Sorocaba","Vale do Paraiba e Litoral Norte"
    }.OrderBy(s => s);

    public static IEnumerable<string> AglomeradosUrbanosSP() => new List<string>()
    {
        "Jundiai","Piracicaba"
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

    public static readonly Dictionary<string, IEnumerable<string>> MReigoesGovernoSP = new()
    {
        {"bauru", RegiaoGovernoBauruSP()},
        {"lins", RegiaoGovernoLinsSP()},
        {"jau", RegiaoGovernoJauSP()},
        {"presidente-prudente", RGPresidentePrudenteSP()},
        {"dracena", RGDracenaSP()},
        {"adamantina", RGAdamantinaSP()},
        {"aracatuba", RGAracatubaSP()},
        {"andradina", RGAndradinaSP()},
        {"sao-jose-do-rio-preto", RGSaoJoseRioPretoSP()},
        {"votuporanga", RGVotuporangaSP()},
        {"fernandopolis", RGFernandopolisSP()},
        {"jales", RGJalesSP()},
        {"catanduva", RGCatanduvaSP()},
        {"barretos", MacroRegiaoBarretosSP()},
        {"franca", RGFrancaSP()},
        {"sao-joaquim-da-barra", RGSaoJoaquimBarraSP()},
        {"ribeirao-preto", MacroRegiaoRibeiraoPretoSP()},
        {"araraquara", RGAraraquaraSP()},
        {"sao-carlos", RGSaoCaorlosSP()},
        {"marilia", RGMariliaSP()},
        {"tupa", RGTupaSP()},
        {"assis", RGAssisSP()},
        {"ourinhos", RGOurinhosSP()},
        {"avare", RGAvareSP()},
        {"itapeva", RGItapevaSP()},
        {"sorocaba",RGSorocabaSP()},
        {"itapetininga", RGItapetiningaSP()},
        {"botucatu", RGBotucatuSP()},
        {"rio-claro", RGRioClaroSP()},
        {"limeira", RGLimeiraSP()},
        {"sao-joao-boavista", RGSaoJoaoBoaVistaSP()},
        {"piracicaba", RGPiracicabaSP()},
        {"campinas", RGCampinasSP()},
        {"braganca-paulista", RGBragancaPaulistaSP()},
        {"jundiai", RGJundiaiSP()},
        {"registro", MacroRegiaoRegistroSP()},
        {"santos", MacroRegiaoSantosSP()},
        {"sao-paulo", MacroRegiaoSaoPauloSP()},
        {"sao-jose-dos-campos",RGSaoJoseCamposSP()},
        {"caraguatatuba",RGCaraguatatubaSP()},
        {"taubate",RGTaubateSP()},
        {"guaratingueta",RGGuaratinguetaSP()},
        {"Cruzeiro",RGCruzeiroSP()}
    };

    public static readonly Dictionary<string, IEnumerable<string>> RMetropolitanasSP = new()
    {
        {"sorocaba", RMSorocabaSP()},
        {"sao-paulo", MacroRegiaoSaoPauloSP()},
        {"campinas", RMCampinasSP()},
        {"vale-do-paraiba-e-litoral-norte", MacroRegiaoSaoJosedosCampos()},
        {"baixada-santista", MacroRegiaoSantosSP()}
    };

    public static readonly Dictionary<string, IEnumerable<string>> AUrbanosSP = new()
    {
        {"jundiai", AUJundiaiSP()},
        {"piracicaba", AUPiracicabaSP()}
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

    private static IEnumerable<string> RegiaoGovernoJauSP()
    => new List<string>() {
        "Itaju",
        "Bariri",
        "Boraceia",
        "Itapui",
        "Bocaina",
        "Jau",
        "Igaracu do Tiete","Barra Bonita","Mineiros do Tiete","Dois Corregos"
    }.OrderBy(s => s);

    private static IEnumerable<string> RegiaoGovernoBauruSP()
    => new List<string>() {
            "Pirajui", "Balbinos","Presidente Alves","Reginopolis","Avai","Iacanga","Ubirajara","Lucianopolis","Duartina","Bauru",
            "Arealva", "Paulistania","Cabralia Paulista","Piratininga", "Pederneiras","Agudos","Borebi","Lencois Paulista", "Macatuba"
    }.OrderBy(s => s);

    private static IEnumerable<string> RegiaoGovernoLinsSP() => new List<string>() {
        "Getulina", "Promissao","Guaicara","Sabino","Guaimbe","Lins","Cafelandia","Guaranta","Pongai","Uru"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGPresidentePrudenteSP() => new List<string>(){
        "Rosana","Presidente Epitacio","Euclides da Cunha Paulista","Teodoro Sampaio","Mirante do Paranapanema","Maraba Paulista",
        "Santo Anastacio","Piquerobi","Presidente Venceslau","Caiua","Ribeirao dos Indios","Sandovalina","Presidente Bernardes",
        "Emilianopolis","Santo Expedito","Alfredo Marcondes","Alvares Machado","Tarabai","Estrela do Norte","Pirapozinho","Presidente Prudente","Narandiba","Anhumas","Regente Feijo","Indiana","Caiabu","Martinopolis","Taciba","Nantes","Iepe","Rancharia"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGDracenaSP() => new List<string>() {
        "Pauliceia","Panorama","Ouro Verde","Santa Mercedes","Sao Joao do Pau d'Alho","Monte Castelo",
        "Nova Guataporanga","Tupi Paulista","Dracena","Junqueiropolis"
     }.OrderBy(s => s);

    private static IEnumerable<string> RGAdamantinaSP() => new List<string>() {
        "Flora Rica","Rapuru","Pacaembu","Florida Paulista","Mariapolis","Adamantina","Pracinha","Lucelia",
        "Salmourao","Inubia Paulista","Sagres","Osvaldo Cruz"
     }.OrderBy(s => s);

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

    private static IEnumerable<string> RGAracatubaSP() => new List<string>() {
        "Luiziania","Alto Alegre","Avanhandava","Barbosa","Penapolis","Santopolis do Aguapei","Clementina","Brauna",
        "Coroados","Glicerio","Brejo Alegre","Piacatu","Gabriel Monteiro","bilac","Birigui","Buritama","Turiuba",
        "Gastao Vidigal","Nova Luzitania","Lourdes","Santo Antonio do Aracangua","Aracatuba","Guararapes","Rubiacea",
        "Bento de Abreu","Valparaiso","Nova Castilho","General Salgado","Sao Joao de Iracema","Auriflama","Guzolandia"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGAndradinaSP() => new List<string>() {
        "Lavinia", "Mirandopolis", "Guaracai", "Murutinga do Sul","Nova Independencia","Castilho","Itapura",
        "Ilha Solteira","Suzanopolis","Pereira Barreto","Sud Mennuci","Andradina"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGSaoJoseRioPretoSP() => new List<string>(){
        "Sao Jose do Rio Preto","Paulo de Faria","Orindiuva","Icem","Palestina","Nova Granada","Onda Verde",
        "Guapiacu","Ipigua","Mirassolandia","Tanabi","Balsamo","Mirassol","bady Bassitt","Cedral","Uchoa","Ibira",
        "Potirendaba","Nova Alianca","Jaci","Neves Paulista","Monte Aprazivel","Poloni","Nipoa","Uniao Paulista",
        "Planalto","Zacarias","Jose Bonifacio","Ubarana","Mendonca","Adolfo"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGVotuporangaSP() => new List<string>(){
        "Votuporanga","Cardoso","Riolandia", "Pontes Gestal","Parisi","Alvares Florence","Americo de Campos",
        "Valentim Gentil" ,"Cosmoranga","Magda","Floreal","Nhandeara","Sebastianopolis do Sul","Macaubal",
        "Moncoes"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGFernandopolisSP() => new List<string>(){
        "Populina","Ouroeste","Indiapora","Mira Estrela","Turmalina","Guarani d'Oeste" ,"Macedonia",
        "Estrela d'Oeste","Fernandopolis","Pedranopolis","Sao Joao das Duas Pontes","Meridiano"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGJalesSP() => new List<string>(){
        "Pontalinda","Dirce Reis","Palmeira d'Oeste","Marinopolis","Aparecida d'Oeste","Rubineia","Santa Fe do Sul",
        "Nova Canaa Paulista","Santana da Ponte Pensa","Tres Fronteiras","Aspasia","Santa Salete","Urania","Jales",
        "Vitoria Brasil","Dolcinopolis","Paranapua","Mesopolis","Santa Albertina","Santa Clara d'Oeste","Santa Rita d'Oeste",
        "Sao Francisco"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGCatanduvaSP() => new List<string>(){
        "Novo Horizonte", "Sales","Irapua","Urupes","Marapoama","Itajobi","Elisiario","Catanduva","Pindorama",
        "Santa Adelia","Ariranha","Palmares Paulista","Paraiso","Novais","Catigua","Tabapua"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGFrancaSP() => new List<string>(){
        "Aramina","Batatais","Buritizal","Cristais Paulista","Franca","Guará","Igarapava",
        "Itirapuã","Ituverava","Jeriquara","Miguelópolis",
        "Patrocínio Paulista","Pedregulho","Restinga","Ribeirão Corrente","Rifaina",
        "São José da Bela Vista"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGSaoJoaquimBarraSP() => new List<string>(){
        "Ipua","Morro Agudo","Nuporanga","Orlândia",
        "Sales Oliveira","São Joaquim da Barra"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGAraraquaraSP() => new List<string>()
    {
        "Américo Brasiliense","Araraquara","Boa Esperança do Sul","Borborema","Cândido Rodrigues",
        "Dobrada","Fernando Prestes","Gavião Peixoto",
        "Ibitinga","Itápolis","Matão","Motuca","Nova Europa",
        "Rincão","Santa Ernestina","Santa Lúcia",
        "Tabatinga","Taquaritinga","Trabiju"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGSaoCaorlosSP() => new List<string>()
    {
        "Dourado","Ribeirão Bonito","Ibaté","São Carlos","Descalvado",
        "Porto Ferreira","Santa Rita do Passa Quatro"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGMariliaSP() => new List<string>(){
        "Pompeia","Oriente","Oscar Bressane","Echapora","Ocauçu",
        "Vera Cruz","Alvaro de Carvalho","Julio Mesquita","Garca",
        "Galia","Lupercio","Fernao","Alvinlandia","Marilia"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGTupaSP() => new List<string>(){
        "Rinopolis","Parapua","Bastos","Iacri","Arco-Iris","Queiroz","Tupa","Herculandia","Quintana",
        "Bora","Quata","Joao Ramalho"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGAssisSP() => new List<string>(){
        "Lutecia","Paraguacu Paulista","Maracai","Cruzalia","Pedrinhas Paulista","Florinea",
        "Taruma","Assis","Platina","Palmital","Ibirarema","Campos Novos Paulista",
        "Candido Mota",
    }.OrderBy(s => s);

    private static IEnumerable<string> RGOurinhosSP() => new List<string>(){
        "Ribeirao do Sul","Salto Grande","Ourinhos","Sao Pedro do Turvo","Espirito Santo do Turvo",
        "Santa Cruz do Rio Pardo","Canitar","Chavantes","Timburi","Ipaussu","Bernardino de Campos",
        "Oleo",
    }.OrderBy(s => s);

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
    }.OrderBy(s => s);

    private static IEnumerable<string> RGBotucatuSP() => new List<string>()
    {
        "Itatinga", "Botucatu", "Pratania","Sao Manuel","Areiopolis","Anhembi","Conchas","Laranjal Paulista",
        "Pereiras","Porangaba","Torre de Pedra","Bofete","Pardinho"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGItapetiningaSP() => new List<string>()
    {
        "Angatuba","Campina do Monte Alegre","Itapetininga","Sao Miguel Arcanjo","Sarapui","Alambari","Capela do Alto","Tatui",
        "Boituva","Cerquilho","Cesario Lange","Quadra","Guarei"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGSorocabaSP() => new List<string>()
    {
        "Jumirim","Tiete","Porto Feliz","Salto","Itu","Aracariguama","Sao Roque","Mairinque","Ibiuna","Aluminio",
        "Votorantim","Sorocaba","Ipero","Aracoiaba da Serra","Salto de Pirapora","Piedade","Pilar do Sul","Tapirai"
    }.OrderBy(s => s);

    private static IEnumerable<string> RMSorocabaSP() => new List<string>()
    {
        "Jumirim","Tiete","Porto Feliz","Salto","Itu","Aracariguama","Sao Roque","Mairinque","Ibiuna","Aluminio",
        "Votorantim","Sorocaba","Ipero","Aracoiaba da Serra","Salto de Pirapora","Piedade","Pilar do Sul","Tapirai",
        "Cerquilho","Cesario Lange","Tatui","Boituva","Capela do Alto","Alambari","Sarapui","Sao Miguel Arcanjo"
    }.OrderBy(s => s);

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
    }.OrderBy(s => s);

    private static IEnumerable<string> RGAvareSP() => new List<string>()
    {
        "Avare","Paranapanema","Itai", "Taquarituba","Coronel Macedo","Itaporanga","Barao de Antonina","fartura","Sarutaia",
        "Taguai","Piraju","Tejupa","Manduri","Aguas de Santa Barbara","Iaras","Cerqueira Cesar","Arandu"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGItapevaSP() => new List<string>()
    {
        "Itapeva","Buri","Taquarivai","Itabera","Riversul","Itarare","Bom Sucesso de Itarare","Nova Campina","Ribeirao Branco","Apiai",
        "Itaoca","Ribeira","Itapirapua Ribeira","Barra do Chapeu","Capao Bonito","Guapiara","Ribeirao Grande","Itapirapua Paulista",
        "Iporanga"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGRioClaroSP() => new List<string>() {
        "Torrinha","Brotas","Itirapina","Ipeuna","Santa Gertrudes",
        "Rio Claro","Corumbatai","Analandia"
     }.OrderBy(s => s);

    private static IEnumerable<string> RGLimeiraSP() => new List<string>(){
        "Pirassununga","Santa Cruz da Conceicao","Leme","Araras",
        "Conchal","Cordeiropolis","Iracemapolis","Limeira"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGSaoJoaoBoaVistaSP() => new List<string>(){
        "Tambau","Mococa","Santa Cruz das Palmeiras","Casa Branca","Itobi","Sao Jose do Rio Pardo","Tapiratiba",
        "Caconde","Divinolandia","Sao Sebastiao da Grama","Itobi","Casa Branca","Aguai","Vargem Grande do Sul",
        "Sao Joao da Boa Vista", "Aguas da Prata","Santo Antonio do Jardim","Espirito Santo do Pinhal"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGPiracicabaSP() => new List<string>(){
        "Piracicaba","Santa Maria da Serra","Sao Pedro","Aguas de Sao Pedro","Charqueada","Saltinho","Rio das Pedras",
        "Mombuca","Rafard","Capivari","Elias Fausto"
    }.OrderBy(s => s);

    private static IEnumerable<string> AUPiracicabaSP() => new List<string>(){
        "Piracicaba","Santa Maria da Serra","Sao Pedro","Aguas de Sao Pedro","Charqueada","Saltinho","Rio das Pedras",
        "Mombuca","Rafard","Capivari","Elias Fausto","Ipeuna","Santa Gertrudes","Iracemapolis","Limeira",
        "Cordeiropolis","Araras","Conchal","Leme","Analandia","Corumbatai","Rio Claro","Ipeuna","Santa Gertrudes"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGCampinasSP() => new List<string>(){
        "Mogi-Guacu","Estiva Gerbi","Itapira","Mogi Mirim","Engenheiro Coelho","Arthur Nogueira","Santo Antonio de Posse","Cosmopolis",
        "Holambra","Jaguariuna","Pedreira","Americana","Paulinia","Campinas","Santa Barbara d'Oeste","Sumare","Hortolandia",
        "Monte Mor","Indaiatuba","Valinhos","Vinhedo","Nova Odessa","Artur Nogueira"
    }.OrderBy(s => s);

    private static IEnumerable<string> RMCampinasSP() => new List<string>(){
        "Engenheiro Coelho","Arthur Nogueira","Santo Antonio de Posse","Cosmopolis",
        "Holambra","Jaguariuna","Pedreira","Americana","Paulinia","Campinas","Santa Barbara d'Oeste","Sumare","Hortolandia",
        "Monte Mor","Nova Odessa","Artur Nogueira","Indaiatuba","Valinhos","Vinhedo","Itatiba","Morungaba"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGJundiaiSP() => new List<string>(){
        "Morungaba","Itatiba","Jarinu","Campo Limpo Paulista","Varzea Paulista","Jundiai","Cabreuva",
        "Itupeva","Louveira"
    }.OrderBy(s => s);

    private static IEnumerable<string> AUJundiaiSP() => new List<string>(){
        "Jarinu","Campo Limpo Paulista","Varzea Paulista","Jundiai","Cabreuva",
        "Itupeva","Louveira"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGBragancaPaulistaSP() => new List<string>(){
        "Atibaia","Bom Jesus dos Perdoes","Nazare Paulista","Piracaia","Braganca Paulista","Vargem","Joanopolis",
        "Tuiuti","Pinhalzinho","Pedra Bela","Amparo","Monte Alegre do Sul","Socorro","Serra Negra","Lindoia",
        "Aguas de Lindoia"
    }.OrderBy(s => s);

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

    private static IEnumerable<string> RGSaoJoseCamposSP() => new List<string>() {
        "Paraibuna", "Santa Branca","Jacarei", "Igarata", "Sao Jose dos Campos",
        "Monteiro Lobato","Cacapava","Jambeiro"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGCaraguatatubaSP() => new List<string>() {
        "Sao Sebastiao","Ilha Bela","Caraguatatuba","Ubatuba"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGTaubateSP() => new List<string>() {
        "Sao Bento do Sapucai","Campos do Jordao","Santo Antonio do Pinhal","Pindamonhangaba",
        "Tremembe","Taubate","Lagoinha","Redencao da Serra","Sao Luiz do Paraitinga","Natividade da Serra"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGGuaratinguetaSP() => new List<string>() {
        "Cunha","Roseira","Aparecida","Potim","Guaratingueta","Lorena","Canas","Cachoeira Paulista","Piquete"
    }.OrderBy(s => s);

    private static IEnumerable<string> RGCruzeiroSP() => new List<string>() {
        "Cruzeiro","Lavrinhas","Queluz","Silveiras","Areias","Sao Jose do Barreiro","Arapei","Bananal"
    }.OrderBy(s => s);

}