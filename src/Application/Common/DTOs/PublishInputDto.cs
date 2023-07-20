namespace UserPermission.API.Application.Common.DTOs
{
    public class PublishInputDto<T>
    {
        public Guid Id { get; }
        public string Operation { get; set; }
        public T Data { get; set; }

        public PublishInputDto(string operation, T data)
        {
            Id = Guid.NewGuid();
            Operation = operation;
            Data = data;
        }
    }
}
