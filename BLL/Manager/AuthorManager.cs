﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

using BLL.Abstract.Helper;
using BLL.Abstract.Manager.Projection;
using BLL.Factory;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class AuthorManager : IAuthorManager
  {
    public IRepository<AuthorModel> Repository { get; } = AuthorRepositoryFactory.GetRepository();

    public AuthorProjection Project(AuthorModel model)
      => new AuthorProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate == DateTime.MinValue,
        FName = model.FName,
        LName = model.LName,
        BirthDate = model.BirthDate,
        ImagePath = model.ImagePath,
        Biography = model.Biography
      };

    public AuthorModel Model(AuthorProjection projection)
      => new AuthorModel
      {
        ID = projection.ID,
        FName = projection.FName,
        LName = projection.LName,
        BirthDate = projection.BirthDate,
        ImagePath = projection.ImagePath,
        Biography = projection.Biography
      };

    public AuthorProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      AuthorModel model = availabilityCheck
        ? (Repository as IAuthorRepository).ReadByIDAvailable(ID)
        : (Repository as IAuthorRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<AuthorProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IAuthorRepository).ReadAllAvailable().Select(Project)
      : (Repository as IAuthorRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy)
      => (Repository as IAuthorRepository).Delete(ID, deletedBy);

    public Int32 Update(AuthorProjection projection, HttpPostedFileBase file, Int32 updatedBy)
    {
      if (!(file is null))
      {
        IImageSaver imageSaver = ImageSaverFactory.GetImageSaver();
        imageSaver.File = file;
        imageSaver.SaveImageAs();

        projection.ImagePath = imageSaver.RelativePath;
      }
      else
      {
        projection.ImagePath = GetByID(projection.ID).ImagePath;
      }

      return (Repository as IAuthorRepository).Update(projection.ID, Model(projection), updatedBy);
    }

    public Int32 Create(AuthorProjection projection, HttpPostedFileBase file, Int32 createdBy)
    {
      if (!(file is null))
      {
        IImageSaver imageSaver = ImageSaverFactory.GetImageSaver();
        imageSaver.File = file;
        imageSaver.SaveImageAs();

        projection.ImagePath = imageSaver.RelativePath;
      }

      return (Repository as IAuthorRepository).Create(Model(projection), createdBy);
    }
  }
}
