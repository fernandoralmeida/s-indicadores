using System.Globalization;

namespace IDN.Core.Helpers;

public static class Dictionaries
{
    public static readonly Dictionary<string, string> PorteEmpresa = new()
    {
        {"00","N/I"},
        {"01","ME"},
        {"02","ME"},
        {"03","EPP"},
        {"05","Demais"}
    };
    public static readonly Dictionary<string, string> MatrizOuFilial = new()
    {
        {"1", "Matriz"},
        {"2", "Filial"}
    };

    /// <summary>
    /// 
    /// </summary>
    public static readonly Dictionary<string, string> SituacaoCadastral = new()
    {
        {"01", "Nula"},
        {"02", "Ativa"},
        {"03", "Suspensa"},
        {"04", "Inapta"},
        {"08", "Baixada"}
    };

    public static readonly Dictionary<string, string> CitiesGroup = new()
    {
        {"6607","Jahu"},
        {"6501","Igaraçu do Tiete"},
        {"6235","Bocaina"},
        {"6559","Itapui"},
        {"6245","Boraceia"},
        {"6203","Bariri"},
        {"6541","Itaju"},
        {"6205","Barra Bonita"},
        {"7195","Torrinha"},
        {"6259","Brotas"},
        {"6697","Mineiros do Tiete"},
        {"6835","Pederneiras"},
        {"6383","Dois Córregos"},
        {"6219","Bauru"}
    };

    public static readonly Dictionary<string, string> AgeGroup = new()
    {
        {"0", "Não se aplica"},
        {"1", "0 a 12 anos"},
        {"2", "13 a 20 anos"},
        {"3", "21 a 30 anos"},
        {"4", "31 a 40 anos"},
        {"5", "41 a 50 anos"},
        {"6", "51 a 60 anos"},
        {"7", "61 a 70 anos"},
        {"8", "71 a 80 anos"},
        {"9", "Maior de 80 anos"}
    };

    public static readonly Dictionary<int, string> AgeGroupCorp = new()
    {
        { 5, "00 a 04 anos" },
        { 10, "05 a 09 anos" },
        { 20, "10 a 19 anos" },
        { 30, "20 a 29 anos" },
        { 40, "30 a 39 anos" },
        { 50, "40 a 49 anos" },
        { int.MaxValue, "50 ou mais anos" }
    };

    public static readonly Dictionary<string, string> SetorProdutivo = new()
    {
        {"00","*"},
        {"01","Agro"},
        {"02","Agro"},
        {"03","Agro"},
        {"05","Industria Extrativa"},{"06","Industria Extrativa"},{"07","Industria Extrativa"},{"08","Industria Extrativa"},{"09","Industria Extrativa"},
        {"10","Industria Transformacao"},{"11","Industria Transformacao"},{"12","Industria Transformacao"},{"13","Industria Transformacao"},{"14","Industria Transformacao"},{"15","Industria Transformacao"},{"16","Industria Transformacao"},{"17","Industria Transformacao"},{"18","Industria Transformacao"},{"19","Industria Transformacao"},
        {"20","Industria Transformacao"},{"21","Industria Transformacao"},{"22","Industria Transformacao"},{"23","Industria Transformacao"},{"24","Industria Transformacao"},{"25","Industria Transformacao"},{"26","Industria Transformacao"},{"27","Industria Transformacao"},{"28","Industria Transformacao"},{"29","Industria Transformacao"},
        {"30","Industria Transformacao"},{"31","Industria Transformacao"},{"32","Industria Transformacao"},{"33","Industria Transformacao"},
        {"35","Servicos"},
        {"36","Servicos"},{"37","Servicos"},{"38","Servicos"},{"39","Servicos"},
        {"41","Construcao Civil"},{"42","Construcao Civil"},{"43","Construcao Civil"},
        {"45","Comercio"},{"46","Comercio"},{"47","Comercio"},
        {"49","Servicos"},{"50","Servicos"},{"51","Servicos"},{"52","Servicos"},{"53","Servicos"},
        {"55","Servicos"},{"56","Servicos"},
        {"58","Servicos"},{"59","Servicos"},{"60","Servicos"},{"61","Servicos"},{"62","Servicos"},{"63","Servicos"},
        {"64","Servicos"},{"65","Servicos"},{"66","Servicos"},
        {"68","Servicos"},
        {"69","Servicos"},{"70","Servicos"},{"71","Servicos"},{"72","Servicos"},{"73","Servicos"},{"74","Servicos"},{"75","Servicos"},
        {"77","Servicos"},{"78","Servicos"},{"79","Servicos"},{"80","Servicos"},{"81","Servicos"},{"82","Servicos"},
        {"84","Adm Publica"},
        {"85","Servicos"},
        {"86","Servicos"},{"87","Servicos"},{"88","Servicos"},
        {"90","Servicos"},{"91","Servicos"},{"92","Servicos"},{"93","Servicos"},
        {"94","Organizacoes Associativas"},
        {"95","Servicos"},{"96","Servicos"},
        {"97","Servicos"},
        {"99","Organismos internacionais"}
    };

    public static readonly Dictionary<string, string> CnaesSubClasses = new()
    {
            {"01","AGRICULTURA, PECUÁRIA E SERVIÇOS RELACIONADOS"},
            {"02","PRODUÇÃO FLORESTAL"},
            {"03","PESCA E AQÜICULTURA"},
            {"05","EXTRAÇÃO DE CARVÃO MINERAL"},
            {"06","EXTRAÇÃO DE PETRÓLEO E GÁS NATURAL"},
            {"07","EXTRAÇÃO DE MINERAIS METÁLICOS"},
            {"08","EXTRAÇÃO DE MINERAIS NÃO-METÁLICOS"},
            {"09","ATIVIDADES DE APOIO À EXTRAÇÃO DE MINERAIS"},
            {"10","FABRICAÇÃO DE PRODUTOS ALIMENTÍCIOS"},
            {"11","FABRICAÇÃO DE BEBIDAS"},
            {"12","FABRICAÇÃO DE PRODUTOS DO FUMO"},
            {"13","FABRICAÇÃO DE PRODUTOS TÊXTEIS"},
            {"14","CONFECÇÃO DE ARTIGOS DO VESTUÁRIO E ACESSÓRIOS"},
            {"15","PREPARAÇÃO DE COUROS E FABRICAÇÃO DE ARTEFATOS DE COURO, ARTIGOS PARA VIAGEM E CALÇADOS"},
            {"16","FABRICAÇÃO DE PRODUTOS DE MADEIRA"},
            {"17","FABRICAÇÃO DE CELULOSE, PAPEL E PRODUTOS DE PAPEL"},
            {"18","IMPRESSÃO E REPRODUÇÃO DE GRAVAÇÕES"},
            {"19","FABRICAÇÃO DE COQUE, DE PRODUTOS DERIVADOS DO PETRÓLEO E DE BIOCOMBUSTÍVEIS"},
            {"20","FABRICAÇÃO DE PRODUTOS QUÍMICOS"},
            {"21","FABRICAÇÃO DE PRODUTOS FARMOQUÍMICOS E FARMACÊUTICOS"},
            {"22","FABRICAÇÃO DE PRODUTOS DE BORRACHA E DE MATERIAL PLÁSTICO"},
            {"23","FABRICAÇÃO DE PRODUTOS DE MINERAIS NÃO-METÁLICOS"},
            {"24","METALURGIA"},
            {"25","FABRICAÇÃO DE PRODUTOS DE METAL, EXCETO MÁQUINAS E EQUIPAMENTOS"},
            {"26","FABRICAÇÃO DE EQUIPAMENTOS DE INFORMÁTICA, PRODUTOS ELETRÔNICOS E ÓPTICOS"},
            {"27","FABRICAÇÃO DE MÁQUINAS, APARELHOS E MATERIAIS ELÉTRICOS"},
            {"28","FABRICAÇÃO DE MÁQUINAS E EQUIPAMENTOS"},
            {"29","FABRICAÇÃO DE VEÍCULOS AUTOMOTORES, REBOQUES E CARROCERIAS"},
            {"30","FABRICAÇÃO DE OUTROS EQUIPAMENTOS DE TRANSPORTE, EXCETO VEÍCULOS AUTOMOTORES"},
            {"31","FABRICAÇÃO DE MÓVEIS"},
            {"32","FABRICAÇÃO DE PRODUTOS DIVERSOS"},
            {"33","MANUTENÇÃO, REPARAÇÃO E INSTALAÇÃO DE MÁQUINAS E EQUIPAMENTOS"},
            {"35","ELETRICIDADE, GÁS E OUTRAS UTILIDADES"},
            {"36","CAPTAÇÃO, TRATAMENTO E DISTRIBUIÇÃO DE ÁGUA"},
            {"37","ESGOTO E ATIVIDADES RELACIONADAS"},
            {"38","COLETA, TRATAMENTO E DISPOSIÇÃO DE RESÍDUOS; RECUPERAÇÃO DE MATERIAIS"},
            {"39","DESCONTAMINAÇÃO E OUTROS SERVIÇOS DE GESTÃO DE RESÍDUOS"},
            {"41","CONSTRUÇÃO DE EDIFÍCIOS"},
            {"42","OBRAS DE INFRA-ESTRUTURA"},
            {"43","SERVIÇOS ESPECIALIZADOS PARA CONSTRUÇÃO"},
            {"45","COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS"},
            {"46","COMÉRCIO POR ATACADO, EXCETO VEÍCULOS AUTOMOTORES E MOTOCICLETAS"},
            {"47","COMÉRCIO VAREJISTA"},
            {"49","TRANSPORTE TERRESTRE"},
            {"50","TRANSPORTE AQUAVIÁRIO"},
            {"51","TRANSPORTE AÉREO"},
            {"52","ARMAZENAMENTO E ATIVIDADES AUXILIARES DOS TRANSPORTES"},
            {"53","CORREIO E OUTRAS ATIVIDADES DE ENTREGA"},
            {"55","ALOJAMENTO"},
            {"56","ALIMENTAÇÃO"},
            {"58","EDIÇÃO E EDIÇÃO INTEGRADA À IMPRESSÃO"},
            {"59","ATIVIDADES CINEMATOGRÁFICAS, PRODUÇÃO DE VÍDEOS E DE PROGRAMAS DE TELEVISÃO; GRAVAÇÃO DE SOM E EDIÇÃO DE MÚSICA"},
            {"60","ATIVIDADES DE RÁDIO E DE TELEVISÃO"},
            {"61","TELECOMUNICAÇÕES"},
            {"62","ATIVIDADES DOS SERVIÇOS DE TECNOLOGIA DA INFORMAÇÃO"},
            {"63","ATIVIDADES DE PRESTAÇÃO DE SERVIÇOS DE INFORMAÇÃO"},
            {"64","ATIVIDADES DE SERVIÇOS FINANCEIROS"},
            {"65","SEGUROS, RESSEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE"},
            {"66","ATIVIDADES AUXILIARES DOS SERVIÇOS FINANCEIROS, SEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE"},
            {"68","ATIVIDADES IMOBILIÁRIAS"},
            {"69","ATIVIDADES JURÍDICAS, DE CONTABILIDADE E DE AUDITORIA"},
            {"70","ATIVIDADES DE SEDES DE EMPRESAS E DE CONSULTORIA EM GESTÃO EMPRESARIAL"},
            {"71","SERVIÇOS DE ARQUITETURA E ENGENHARIA; TESTES E ANÁLISES TÉCNICAS"},
            {"72","PESQUISA E DESENVOLVIMENTO CIENTÍFICO"},
            {"73","PUBLICIDADE E PESQUISA DE MERCADO"},
            {"74","OUTRAS ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS"},
            {"75","ATIVIDADES VETERINÁRIAS"},
            {"77","ALUGUÉIS NÃO-IMOBILIÁRIOS E GESTÃO DE ATIVOS INTANGÍVEIS NÃO-FINANCEIROS"},
            {"78","SELEÇÃO, AGENCIAMENTO E LOCAÇÃO DE MÃO-DE-OBRA"},
            {"79","AGÊNCIAS DE VIAGENS, OPERADORES TURÍSTICOS E SERVIÇOS DE RESERVAS"},
            {"80","ATIVIDADES DE VIGILÂNCIA, SEGURANÇA E INVESTIGAÇÃO"},
            {"81","SERVIÇOS PARA EDIFÍCIOS E ATIVIDADES PAISAGÍSTICAS"},
            {"82","SERVIÇOS DE ESCRITÓRIO, DE APOIO ADMINISTRATIVO E OUTROS SERVIÇOS PRESTADOS PRINCIPALMENTE ÀS EMPRESAS"},
            {"84","ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL"},
            {"85","EDUCAÇÃO"},
            {"86","ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA"},
            {"87","ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA INTEGRADAS COM ASSISTÊNCIA SOCIAL, PRESTADAS EM RESIDÊNCIAS COLETIVAS E PARTICULARES"},
            {"88","SERVIÇOS DE ASSISTÊNCIA SOCIAL SEM ALOJAMENTO"},
            {"90","ATIVIDADES ARTÍSTICAS, CRIATIVAS E DE ESPETÁCULOS"},
            {"91","ATIVIDADES LIGADAS AO PATRIMÔNIO CULTURAL E AMBIENTAL"},
            {"92","ATIVIDADES DE EXPLORAÇÃO DE JOGOS DE AZAR E APOSTAS"},
            {"93","ATIVIDADES ESPORTIVAS E DE RECREAÇÃO E LAZER"},
            {"94","ATIVIDADES DE ORGANIZAÇÕES ASSOCIATIVAS"},
            {"95","REPARAÇÃO E MANUTENÇÃO DE EQUIPAMENTOS DE INFORMÁTICA E COMUNICAÇÃO E DE OBJETOS PESSOAIS E DOMÉSTICOS"},
            {"96","OUTRAS ATIVIDADES DE SERVIÇOS PESSOAIS"},
            {"97","SERVIÇOS DOMÉSTICOS"},
            {"99","ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS"}
    };
}

