import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import './SelectLayouts.css'
import { LayoutManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'

class EventsManagementSelectLayoutsPlain extends Component {
  static displayName = EventsManagementSelectLayoutsPlain.name;

  constructor(props) {
    super(props);
    this.state = { layouts: [], loading: true };
  }

  componentDidMount() {
    this.getLayouts();
  }

  async getLayouts() {
    const LayoutClient = new LayoutManagementApi(EventsManagementApiHTTPSconfig);
    await LayoutClient.apiLayoutManagementLayoutsGet()
      .then(result => this.setState({ layouts: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }
  static renderContent(layouts, props) {
    const { t } = props;
    return (<div className="form-group">
      <div className="wrap">
        <div className="left">
          <table className="table">
            <tbody>
              <tr><th>{t('Name')}</th><th>{t('Description')}</th></tr>
              {layouts.map(layout => (
                <tr key={"tr_".concat(layout.id)}>
                  <td key={"td_name_".concat(layout.id)}>
                    {layout.name}
                    <br key={"br1_".concat(layout.id)} />
                  </td>
                  <td key={"td_description_".concat(layout.id)}>
                    {layout.description}
                    <br key={"br2_".concat(layout.id)} />
                  </td>
                </tr>
              ))}
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
                      {layouts.map(layout => (
                        <Fragment key={"fr_".concat(layout.id)} >
                          <input key={"checkbox_".concat(layout.id)} type="checkbox" name="layoutsId" value="@layout.Id" />
                          <hr key={"hr".concat(layout.id)} />
                        </Fragment>
                      ))}
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
    )
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : EventsManagementSelectLayoutsPlain.renderContent(this.state.layouts, this.props);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const EventsManagementSelectLayouts = withTranslation()(EventsManagementSelectLayoutsPlain);
