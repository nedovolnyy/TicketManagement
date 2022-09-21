import React, { Component, Fragment } from 'react'
import './SelectLayouts.css'
import { withTranslation } from 'react-i18next'
import { LayoutManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { DataNavigation } from 'react-data-navigation'
import { withRouter } from '../../helpers/withRouter'

class EventsManagementSelectLayoutsPlain extends Component {
  static displayName = EventsManagementSelectLayoutsPlain.name;

  constructor(props) {
    super(props);
    this.state = { layouts: [], selectedLayouts: [], loading: true };
  }

  componentDidMount() {
    this.getLayouts();
  }

  getLayouts() {
    let selectedVenues = DataNavigation.getData('selectedVenues');
    const LayoutClient = new LayoutManagementApi(EventsManagementApiHTTPSconfig);
    selectedVenues.map(venue => (
      LayoutClient.apiLayoutManagementLayoutsByVenueIdVenueIdGet(+venue)
        .then(result => this.setState({ layouts: [...this.state.layouts, result], loading: false }))
        .catch((error) => {
          console.log(error);
          alert(error);
        })
    ));
  }

  handleSubmit = (event) => {
    DataNavigation.setData('selectedLayouts', this.state.selectedLayouts);
    this.props.router.navigate('/EventsManagement/Insert');
  }

  handleCheckboxChange = (event) => {
    let newArray = [...this.state.selectedLayouts, event.target.id];
    if (this.state.selectedLayouts.includes(event.target.id)) {
      newArray = newArray.filter(venue => venue !== event.target.id);
    }
    this.setState({
      selectedLayouts: newArray
    });
  }

  renderContent(layoutsFromVenues) {
    const { t } = this.props;
    return (<div className="form-group">
      <div className="wrap">
        <div className="layouts_left">
          <table className="table">
            <tbody>
              <tr><th>{t('Name')}</th><th>{t('Description')}</th></tr>
              {layoutsFromVenues.map(layouts => layouts.map(layout => (
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
              )))}
            </tbody>
          </table>
        </div><div className="layouts_right">
          <table className="table">
            <tbody>
              <tr><th></th></tr>
              <tr>
                <td>
                  <form>
                    <div className="table-active">
                      {layoutsFromVenues.map(layouts => layouts.map(layout => (
                        <Fragment key={"fr_".concat(layout.id)} >
                          <input key={"checkbox_".concat(layout.id)} type="checkbox" id={layout.id} value={layout.id} onChange={this.handleCheckboxChange} />
                          <hr key={"hr".concat(layout.id)} />
                        </Fragment>
                      )))}
                    </div>
                    <button type="button" className="btn btn-primary" onClick={this.handleSubmit}>{t('Select')}</button>
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
      : this.renderContent(this.state.layouts);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const EventsManagementSelectLayouts = withRouter(withTranslation()(EventsManagementSelectLayoutsPlain));
