using AutoMapper;

namespace InscricaoAluno.Api.Mapeamento
{
    public static class MapExtension
    {

        public static DTO.Aluno Map(this IMap map, Models.Aluno aluno)
            => Mapper.Map<DTO.Aluno>(aluno);

        public static Models.Aluno Map(this IMap map, DTO.Aluno aluno)
            => Mapper.Map<Models.Aluno>(aluno);

        public static DTO.Curso Map(this IMap map, Models.Curso curso)
            => Mapper.Map<DTO.Curso>(curso);

        public static Models.Curso Map(this IMap map, DTO.Curso curso)
            => Mapper.Map<Models.Curso>(curso);

        public static DTO.Inscricao Map(this IMap map, Models.Inscricao inscricao)
            => Mapper.Map<DTO.Inscricao>(inscricao);

        public static Models.Inscricao Map(this IMap map, DTO.Inscricao inscricao)
            => Mapper.Map<Models.Inscricao>(inscricao);

        public static DTO.Inscricao Map(this IMap map, Models.Join item)
        {
            DTO.Inscricao inscricao = Map(map, item.Inscricao);
            inscricao.Aluno = Map(map, item.Aluno);
            inscricao.Curso = Map(map, item.Curso);
            return inscricao;
        }
    }

    public interface IMap
    {
    }
}
