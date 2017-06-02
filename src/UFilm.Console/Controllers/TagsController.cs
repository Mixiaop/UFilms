using System;
using System.Collections.Generic;
using U;
using U.FakeMvc.Controllers;
using UFilm.Domain.Tags;
using UFilm.Services.Tags;
using UFilm.Console.Models.Tags;

namespace UFilm.Console.Controllers
{
    public class TagsController : ControllerBase
    {
        public ITagService TagService = UPrimeEngine.Instance.Resolve<ITagService>();

        [HttpGet]
        public TagListModel TagListView(int typeId = 0, string wd = "") {
            TagListModel Model = new TagListModel();
            Model.GetTypeId = typeId;
            Model.GetKeywords = wd;

            //TagService.Search((TagType)Model.GetTypeId)
            return Model;
        }

    }
}