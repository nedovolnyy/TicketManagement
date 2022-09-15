import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"
import './_SelectEventSeatPartial.css'

class VenuesManagementPlain extends Component {
  static displayName = VenuesManagementPlain.name;

  /*
    var eventSeatsCollection = await EventSeatManagementApiClient.GetAllEventSeatsByEventAreaIdAsync(Model.Id);
    var maxRow = eventSeatsCollection.Max(x => x.Row);
    var maxNumber = eventSeatsCollection.Max(x => x.Number);
    var returnUrl = string.IsNullOrEmpty(Context.Request.Path) ? "~/" : $"~{Context.Request.Path.Value}" + Context.Request.QueryString.ToString();
  */

  render() {
    const { t } = this.props;
    return (
      <>
        <table>
          <tbody>
            <div className="input-group">
              {/*
        int tempRow = 0;
        map (var seat in eventSeatsCollection.ToList())
    */}
              {/*if (seat.Row != tempRow)
            */}
              <tr>
                <td>
                  <div className="container">
                    {/* if (User.Identity is not null && User.Identity.IsAuthenticated) */}
                    <form id={string.concat("seat", "seat.Id")} asp-controller="Event"
                      asp-action="Purchase" asp-route-eventSeatId="@seat.Id" asp-route-returnUrl="@returnUrl" asp-route-price="@Model.Price"
                      method="post" className="form-horizontal" role="form">
                      <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" onClick={window.confirm({ t('Are you sure you want to buy this seat?') })} id={"btn" + "seat.Id"} /*((seat.State != State.Available) ? "disabled" : "")*/ >
                        <label className="text-sm-start">@seat.Row</label>
                        {/*Html.DisplayFor(model => model.Price)*/}
                        <label className="text-sm-end">@seat.Number</label>
                      </button>
                    </form>

                    {/*else*/}
                    <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" id={"btn" + "seat.Id"} /*((seat.State != State.Available) ? "disabled" : "")*/>
                      <label className="text-sm-start">@seat.Row</label>
                      {/*@Html.DisplayFor(model => model.Price)*/}
                      <label className="text-sm-end">@seat.Number</label>
                    </button>
                  </div>
                </td>
                {/*if (seat.Row == tempRow)*/}
                <td>
                  <div className="container">
                    {/*if (User.Identity is not null && User.Identity.IsAuthenticated)*/}
                    <form id={"seat" + "seat.Id"} asp-controller="Event"
                      asp-action="Purchase" asp-route-eventSeatId="@seat.Id" asp-route-returnUrl="@returnUrl" asp-route-price="@Model.Price"
                      method="post" className="form-horizontal" role="form">
                      <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" onClick={() => window.confirm({ t('Are you sure you want to buy this seat?') })} id={"btn" + "seat.Id"} /*((seat.State != State.Available) ? "disabled" : "")*/ >
                        <label className="text-sm-start">@seat.Row</label>
                                    @Html.DisplayFor(model => model.Price)
                        <label className="text-sm-end">@seat.Number</label>
                      </button>
                    </form>
                    {/*else*/}
                    <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" id={"btn" + "seat.Id"} /*((seat.State != State.Available) ? "disabled" : "")*/>
                      <label className="text-sm-start">@seat.Row</label>
                      {/*Html.DisplayFor(model => model.Price)*/}
                      <label className="text-sm-end">@seat.Number</label>
                    </button>
                  </div>
                </td>
                {/*tempRow = seat.Row;*/}
              </tr>
            </div>
          </tbody>
        </table>
      </>
    );
  }
}

export const VenuesManagement = withTranslation()(VenuesManagementPlain);
