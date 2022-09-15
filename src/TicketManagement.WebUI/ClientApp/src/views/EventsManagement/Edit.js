import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class EventsManagementEditPlain extends Component {
  static displayName = EventsManagementEditPlain.name;

  /*
    var timezoneList = TimeZoneInfo.GetSystemTimeZones()
        ?.Select(c => new SelectListItem { Value = c.BaseUtcOffset.ToString(), Text = c.DisplayName })
        .ToList();
    var eventId = Context.Request.Query["eventId"];
    var layout = await LayoutManagementApiClient.GetByIdLayoutAsync(int.Parse(Model.LayoutsId[0]));
    var layoutId = layout.Id;
    var selectListLayoutsId = (await LayoutManagementApiClient.GetAllLayoutsAsync())
        ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        .ToList();
  */

  render() {
    const { t } = this.props;
    return (
      <>
        <div className="row">
          <div className="col-md-6">
            <form id="profile-form" method="post" asp-action="Edit" asp-controller="EventsManagement">
              <div asp-validation-summary="ModelOnly" className="text-danger"></div>
              <div className="form-floating">
                <input asp-for="Name" className="form-control" />
                <label asp-for="Name" className="form-label"></label>
                <span asp-validation-for="Name" className="text-danger"></span>
              </div>
              <div className="form-floating">
                <input asp-for="Description" className="form-control" />
                <label asp-for="Description" className="form-label"></label>
                <span asp-validation-for="Description" className="text-danger"></span>
              </div>
              <div className="form-floating">
                <input asp-for="EventTime" className="form-control" asp-format="{0:yyyy-MM-ddTHH:mm}" />
                <label asp-for="EventTime" className="form-label"></label>
                <span asp-validation-for="EventTime" className="text-danger"></span>
                <div className="form-floating">
                  <select name="timeZone" asp-items="timezoneList" className="form-select"></select>
                  <label className="form-label">{t('Time Zone for Event')}</label>
                </div>
              </div>
              <div className="form-floating">
                <input asp-for="EventEndTime" className="form-control" asp-format="{0:yyyy-MM-ddTHH:mm}" />
                <label asp-for="EventEndTime" className="form-label"></label>
                <span asp-validation-for="EventEndTime" className="text-danger"></span>
              </div>
              <div className="form-floating">
                <input asp-for="EventLogoImage" className="form-control" />
                <label asp-for="EventLogoImage" className="form-label"></label>
                <span asp-validation-for="EventLogoImage" className="text-danger"></span>
              </div>
              <div className="form-floating">
                {/*
                    if (ViewBag.isAllAvailableSeats)
                    {
                        @Html.DropDownListFor(m=>layoutId, selectListLayoutsId, "Default label", new { @className = "form-select"})
                    }
                    else
                    {
                        <input name="layoutId" value="@layout.Id" className="form-control invisible" hidden></input>
                        <input className="form-control" value="@layout.Name" disabled></input>
                    }
                  */}
                <label className="form-label">{t('Layout')}</label>
              </div>
              <div className="form-floating">
                {/*
                    if (ViewBag.isAllAvailableSeats)
                    {
                        <input asp-for="Price" className="currencyInput form-control"></input>
                    }
                    else
                    {
                        <input asp-for="Price" className="currencyInput form-control" disabled></input>
                    }
                  */}
                <label asp-for="Price" className="form-label"></label>
                <span asp-validation-for="Price" className="text-danger"></span>
              </div>
              <div className="table-active invisible">
                <input name="eventId" value="@eventId" hidden />
              </div>
              <button type="submit" className="w-100 btn btn-lg btn-primary">{t('Edit Event')}</button>
            </form>
          </div>
        </div>
      </>
    );
  }
}

export const EventsManagementEdit = withTranslation()(EventsManagementEditPlain);
