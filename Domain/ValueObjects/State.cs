using System.Collections.Generic;

namespace Domain.ValueObjects
{
  public class State
  {
    public int Code { get; }

    public string Name { get; }

    public string Acronym { get; }

    public State(int code, string name, string acronym)
    {
      Code = code;
      Name = name;
      Acronym = acronym;
    }

    public static IEnumerable<State> States = new List<State>
      {
        new State(11, "Rondônia", "RO"),
        new State(12, "Acre", "AC"),
        new State(13, "Amazonas", "AM"),
        new State(14, "Roraima", "RR"),
        new State(15, "Pará", "PA"),
        new State(16, "Amapá", "AP"),
        new State(17, "Tocantins", "TO"),
        new State(21, "Maranhão", "MA"),
        new State(22, "Piauí", "PI"),
        new State(23, "Ceará", "CE"),
        new State(24, "Rio Grande do Norte", "RN"),
        new State(25, "Paraíba", "PB"),
        new State(26, "Pernambuco", "PE"),
        new State(27, "Alagoas", "AL"),
        new State(28, "Sergipe", "SE"),
        new State(29, "Bahia", "BA"),
        new State(31, "Minas Gerais", "MG"),
        new State(32, "Espírito Santo", "ES"),
        new State(33, "Rio de Janeiro", "RJ"),
        new State(35, "São Paulo", "SP"),
        new State(41, "Paraná", "PR"),
        new State(42, "Santa Catarina", "SC"),
        new State(43, "Rio Grande do Sul", "RS"),
        new State(50, "Mato Grosso do Sul", "MS"),
        new State(51, "Mato Grosso", "MT"),
        new State(52, "Goiás", "GO"),
        new State(53, "Distrito Federal", "DF")
      };
  }
}