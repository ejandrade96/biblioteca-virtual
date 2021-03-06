using System.Collections.Generic;

namespace Domain.ValueObjects
{
  public class StreetType
  {
    public string Code { get; }

    public string Description { get; }

    public StreetType(string code)
    {
      Code = code;
    }

    private StreetType(string code, string description)
    {
      Code = code;
      Description = description;
    }

    public static IEnumerable<StreetType> StreetTypes = new List<StreetType>
      {
        new StreetType ("A", "Área"),
        new StreetType ("A", "Área"),
        new StreetType ("AC", "Acesso"),
        new StreetType ("ACA", "Acampamento"),
        new StreetType ("ACL", "Acesso Local"),
        new StreetType ("AD", "Adro"),
        new StreetType ("AE", "Área Especial"),
        new StreetType ("AER", "Aeroporto"),
        new StreetType ("AL", "Alameda"),
        new StreetType ("AMD", "Avenida Marginal Direita"),
        new StreetType ("AME", "Avenida Marginal Esquerda"),
        new StreetType ("AN", "Anel Viário"),
        new StreetType ("ANT", "Antiga Estrada"),
        new StreetType ("ART", "Artéria"),
        new StreetType ("AT", "Alto"),
        new StreetType ("ATL", "Atalho"),
        new StreetType ("A V", "Área Verde"),
        new StreetType ("AVC", "Avenida Contorno"),
        new StreetType ("AVM", "Avenida Marginal"),
        new StreetType ("AVV", "Avenida Velha"),
        new StreetType ("BAL", "Balneário"),
        new StreetType ("BC", "Beco"),
        new StreetType ("BCO", "Buraco"),
        new StreetType ("BEL", "Belvedere"),
        new StreetType ("BL", "Bloco"),
        new StreetType ("BLO", "Balão"),
        new StreetType ("BLS", "Blocos"),
        new StreetType ("BLV", "Bulevar"),
        new StreetType ("BSQ", "Bosque"),
        new StreetType ("BVD", "Boulevard"),
        new StreetType ("BX", "Baixa"),
        new StreetType ("C", "Cais"),
        new StreetType ("CAL", "Calçada"),
        new StreetType ("CAM", "Caminho"),
        new StreetType ("CAN", "Canal"),
        new StreetType ("CH", "Chácara"),
        new StreetType ("CHA", "Chapadão"),
        new StreetType ("CIC", "Ciclovia"),
        new StreetType ("CIR", "Circular"),
        new StreetType ("CJ", "Conjunto"),
        new StreetType ("CJM", "Conjunto Mutirão"),
        new StreetType ("CMP", "Complexo Viário"),
        new StreetType ("COL", "Colônia"),
        new StreetType ("COM", "Comunidade"),
        new StreetType ("CON", "Condomínio"),
        new StreetType ("COR", "Corredor"),
        new StreetType ("CPO", "Campo"),
        new StreetType ("CRG", "Córrego"),
        new StreetType ("CTN", "Contorno"),
        new StreetType ("DSC", "Descida"),
        new StreetType ("DSV", "Desvio"),
        new StreetType ("DT", "Distrito"),
        new StreetType ("EB", "Entre Bloco"),
        new StreetType ("EIM", "Estrada Intermunicipal"),
        new StreetType ("ENS", "Enseada"),
        new StreetType ("ENT", "Entrada Particular"),
        new StreetType ("EQ", "Entre Quadra"),
        new StreetType ("ESC", "Escada"),
        new StreetType ("ESD", "Escadaria"),
        new StreetType ("ESE", "Estrada Estadual"),
        new StreetType ("ESI", "Estrada Vicinal"),
        new StreetType ("ESL", "Estrada de Ligação"),
        new StreetType ("ESM", "Estrada Municipal"),
        new StreetType ("ESP", "Esplanada"),
        new StreetType ("ESS", "Estrada de Servidão"),
        new StreetType ("EST", "Estrada"),
        new StreetType ("ESV", "Estrada Velha"),
        new StreetType ("ETA", "Estrada Antiga"),
        new StreetType ("ETC", "Estação"),
        new StreetType ("ETD", "Estádio"),
        new StreetType ("ETN", "Estância"),
        new StreetType ("ETP", "Estrada Particular"),
        new StreetType ("ETT", "Estacionamento"),
        new StreetType ("EVA", "Evangélica"),
        new StreetType ("EVD", "Elevada"),
        new StreetType ("EX", "Eixo Industrial"),
        new StreetType ("FAV", "Favela"),
        new StreetType ("FAZ", "Fazenda"),
        new StreetType ("FER", "Ferrovia"),
        new StreetType ("FNT", "Fonte"),
        new StreetType ("FRA", "Feira"),
        new StreetType ("FTE", "Forte"),
        new StreetType ("GAL", "Galeria"),
        new StreetType ("GJA", "Granja"),
        new StreetType ("HAB", "Núcleo Habitacional"),
        new StreetType ("IA", "Ilha"),
        new StreetType ("IND", "Indeterminado"),
        new StreetType ("IOA", "Ilhota"),
        new StreetType ("JD", "Jardim"),
        new StreetType ("JDE", "Jardinete"),
        new StreetType ("LD", "Ladeira"),
        new StreetType ("LGA", "Lagoa"),
        new StreetType ("LGO", "Lago"),
        new StreetType ("LOT", "Loteamento"),
        new StreetType ("LRG", "Largo"),
        new StreetType ("LT", "Lote"),
        new StreetType ("MER", "Mercado"),
        new StreetType ("MNA", "Marina"),
        new StreetType ("MOD", "Modulo"),
        new StreetType ("MRG", "Projeção"),
        new StreetType ("MRO", "Morro"),
        new StreetType ("MTE", "Monte"),
        new StreetType ("NUC", "Núcleo"),
        new StreetType ("NUR", "Núcleo Rural"),
        new StreetType ("OUT", "Outeiro"),
        new StreetType ("PAR", "Paralela"),
        new StreetType ("PAS", "Passeio"),
        new StreetType ("PAT", "Pátio"),
        new StreetType ("PC", "Praça"),
        new StreetType ("PCE", "Praça de Esportes"),
        new StreetType ("PDA", "Parada"),
        new StreetType ("PDO", "Paradouro"),
        new StreetType ("PNT", "Ponta"),
        new StreetType ("PR", "Praia"),
        new StreetType ("PRL", "Prolongamento"),
        new StreetType ("PRM", "Parque Municipal"),
        new StreetType ("PRQ", "Parque"),
        new StreetType ("PRR", "Parque Residencial"),
        new StreetType ("PSA", "Passarela"),
        new StreetType ("PSG", "Passagem"),
        new StreetType ("PSP", "Passagem de Pedestre"),
        new StreetType ("PSS", "Passagem Subterrânea"),
        new StreetType ("PTE", "Ponte"),
        new StreetType ("PTO", "Porto"),
        new StreetType ("Q", "Quadra"),
        new StreetType ("QTA", "Quinta"),
        new StreetType ("QTS", "Quintas"),
        new StreetType ("R", "Rua"),
        new StreetType ("RAM", "Ramal"),
        new StreetType ("RCR", "Recreio"),
        new StreetType ("REC", "Recanto"),
        new StreetType ("RER", "Retiro"),
        new StreetType ("RES", "Residencial"),
        new StreetType ("RET", "Reta"),
        new StreetType ("RLA", "Ruela"),
        new StreetType ("RMP", "Rampa"),
        new StreetType ("ROA", "Rodo Anel"),
        new StreetType ("ROD", "Rodovia"),
        new StreetType ("ROT", "Rotula"),
        new StreetType ("RPE", "Rua de Pedestre"),
        new StreetType ("RPR", "Margem"),
        new StreetType ("RTN", "Retorno"),
        new StreetType ("RTT", "Rotatória"),
        new StreetType ("SEG", "Segunda Avenida"),
        new StreetType ("SIT", "Sitio"),
        new StreetType ("SRV", "Servidão"),
        new StreetType ("ST", "Setor"),
        new StreetType ("SUB", "Subida"),
        new StreetType ("TCH", "Trincheira"),
        new StreetType ("TER", "Terminal"),
        new StreetType ("TR", "Trecho"),
        new StreetType ("TRV", "Trevo"),
        new StreetType ("TUN", "Túnel"),
        new StreetType ("TV", "Travessa"),
        new StreetType ("TVP", "Travessa Particular"),
        new StreetType ("TVV", "Travessa Velha"),
        new StreetType ("UNI", "Unidade"),
        new StreetType ("V", "Via"),
        new StreetType ("VAC", "Via de Acesso"),
        new StreetType ("VAL", "Vala"),
        new StreetType ("VCO", "Via Costeira"),
        new StreetType ("VD", "Viaduto"),
        new StreetType ("VER", "Vereda"),
        new StreetType ("VEV", "Via Elevado"),
        new StreetType ("VL", "Vila"),
        new StreetType ("VLA", "Viela"),
        new StreetType ("VLE", "Vale"),
        new StreetType ("VLT", "Via Litorânea"),
        new StreetType ("VPE", "Via de Pedestre"),
        new StreetType ("VRT", "Variante"),
        new StreetType ("ZIG", "Zigue-Zague"),
        new StreetType ("AV", "Avenida"),
        new StreetType ("R I", "Rua Integração"),
        new StreetType ("R L", "Rua de Ligação"),
        new StreetType ("R P", "Rua Particular"),
        new StreetType ("R V", "Rua Velha"),
        new StreetType ("V C", "Via Coletora"),
        new StreetType ("V L", "Via Local"),
        new StreetType ("V-E", "Via Expressa"),
    };
  }
}