using Microsoft.AspNetCore.Mvc;
using DicomAdapter.Services;

namespace DicomAdapter.Controllers;

[ApiController]
[Route("api/studies")]
public class StudiesController : ControllerBase
{
    private readonly OrthancService _orthanc;

    public StudiesController(OrthancService orthanc)
    {
        _orthanc = orthanc;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudies()
    {
        var result = await _orthanc.GetStudies();
        return Content(result, "application/json");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudy(string id)
    {
        // Detect if this is a StudyInstanceUID (DICOM UID usually starts with "1.")
        if (id.StartsWith("1."))
        {
            var orthancId = await _orthanc.FindStudyIdByUID(id);

            if (string.IsNullOrEmpty(orthancId))
                return NotFound($"Study with UID {id} not found");

            id = orthancId;
        }

        var result = await _orthanc.GetStudy(id);
        return Content(result, "application/json");
    }
}