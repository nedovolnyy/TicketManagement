import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"
import './SelectLayouts.css'

class EventsManagementSelectLayoutsPlain extends Component {
  static displayName = EventsManagementSelectLayoutsPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <div className="form-group">
          <div className="wrap">
            <div className="left">
              <table className="table">
                <tbody>
                  <tr><th>{t('Name')}</th><th>{t('Description')}</th></tr>
                  {/* map layout */}
                  <tr>
                    <td>
                      @layout.Name;
                      <br />
                    </td>
                    <td>
                      @layout.Description;
                      <br />
                    </td>
                  </tr>
                </tbody>
              </table>
            </div><div className="right">
              <table className="table">
                <tbody>
                  <tr><th></th></tr>
                  <tr>
                    <td>
                      <form asp-action="Insert" asp-controller="EventsManagement" method="get">
                        <input type="hidden" />
                        <div className="table-active">
                          {/* map */}
                          <input type="checkbox" name="layoutsId" value="@layout.Id" checked />
                          <hr />
                        </div>
                        <button type="submit" className="btn btn-primary">{t('Select')}</button>
                      </form>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </>
    );
  }
}

export const EventsManagementSelectLayouts = withTranslation()(EventsManagementSelectLayoutsPlain);
