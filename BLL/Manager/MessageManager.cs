using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
  public class MessageManager : IMessageManager
  {
    public IRepository<MessageModel> Repository { get; } = MessageRepositoryFactory.GetRepository();

    public MessageProjection Project(MessageModel model)
      => new MessageProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        SenderFName = model.SenderFName,
        SenderLName = model.SenderLName,
        SenderEmail = model.SenderEmail,
        SenderDate = model.SenderDate,
        SenderMessage = model.SenderMessage,
        ResponderUserFK = model.ResponderUserFK,
        ResponderDate = model.ResponderDate,
        ResponderMessage = model.ResponderMessage
      };

    public MessageModel Model(MessageProjection projection)
      => new MessageModel
      {
        ID = projection.ID,
        SenderFName = projection.SenderFName,
        SenderLName = projection.SenderLName,
        SenderEmail = projection.SenderEmail,
        SenderDate = projection.SenderDate,
        SenderMessage = projection.SenderMessage,
        ResponderUserFK = projection.ResponderUserFK,
        ResponderDate = projection.ResponderDate,
        ResponderMessage = projection.ResponderMessage
      };

    public MessageProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      MessageModel model = availabilityCheck
        ? (Repository as IMessageRepository).ReadByIDAvailable(ID)
        : (Repository as IMessageRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<MessageProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IMessageRepository).ReadAllAvailable().Select(Project)
      : (Repository as IMessageRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Send(MessageProjection projection)
      => Send(projection.SenderFName, projection.SenderLName, projection.SenderEmail, projection.SenderMessage);
    public Int32 Send(String senderFName, String senderLName, String senderEmail, String senderMessage)
      => (Repository as IMessageRepository).Send(senderFName, senderLName, senderEmail, senderMessage);

    public Int32 Respond(MessageProjection projection)
      => Respond(projection.ID, projection.ResponderUserFK, projection.ResponderMessage);
    public Int32 Respond(Int32 ID, Int32 responderUserFK, String responderMessage)
    {
      MessageProjection message = GetByID(ID);
      if (message == null) return 0;

      Int32 updatedCount = (Repository as IMessageRepository).Respond(ID, responderUserFK, responderMessage);
      if (updatedCount == 0) return 0;

      message.ResponderUserFK = responderUserFK;
      message.ResponderMessage = responderMessage;
      SendRespondMessageEmail(message);

      return updatedCount;
    }

    private void SendRespondMessageEmail(MessageProjection projection)
    {
      String subject = "Odgovor na upit";
      StringBuilder body = new StringBuilder().Append($"Poštovani {projection.SenderFName} {projection.SenderLName},<br /><br />")
                                              .Append($"{projection.ResponderMessage}");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.SenderEmail, $"{projection.SenderFName} {projection.SenderLName}");
      emailSender.SendEmail(subject, body.ToString());
    }
  }
}
