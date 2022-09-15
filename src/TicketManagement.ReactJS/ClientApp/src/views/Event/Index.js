import React from "react"
import { Button, Form } from "reactstrap"
import { useTranslation } from "react-i18next"
import { event } from "../../models/event"
import './Index.css'

export function Event(props) {
  const { t } = useTranslation();
  //eventAreaManagementApiClient.GetAllEventAreasByEventIdAsync(eventId);
  // var evnt = await EventManagementApiClient.GetByIdEventAsync(eventId);

    return (
      <>
        <div className="text-center">
          <h1 className="display-4">{t('Welcome to')} @evnt.Name</h1>
          <div className="img__containerE">
            <img className="image shadow-lg" alt="" src="@Url.Content(evnt.EventLogoImage)" />
            <div className="img__descriptionE">
              <div className="img__headerE"><h2 className="noneDecoration">@evnt.Name</h2></div>
              <div className="text">
                <div className="right">
                  <p>@(await EventManagementApiClient.GetSeatsAvailableCountAsync(evnt.Id)) / @(await EventManagementApiClient.GetSeatsCountAsync(evnt.LayoutId))</p>
                </div>
              </div>
              <div className="text">
                <div className="right">
                  <p className="eventTime">@Localizer["Avalaible"]</p>
                </div>
              </div>
            </div>
          </div>
          <p>{event.description}</p>
        </div>

        <div className="row">
          <table className="table">
            <tbody>
              {/* map (var item in Model) */}
              <tr>
                <td>
                  <div className="text">
                    <div className="container">
                      <div className="container text-right">
                        @await Html.PartialAsync("_SelectEventSeatPartial",item)
                      </div>
                    </div>
                    <div className="right">
                      <p>@item.Description</p>
                    </div>
                  </div>
                  <div className="textE">
                    <div className="right">
                      <p className="eventTime">@Html.ModifyDateTime(evnt.EventTime)</p>
                    </div>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </>
    );
}
