using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace MedicalBlog.Api.Controllers;

[Route("medical-blog")]

public class MedicalBlogController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public MedicalBlogController(
        ISender mediator, 
        IMapper mapper)
    {

        _mediator = mediator;
        _mapper = mapper;
    }


}