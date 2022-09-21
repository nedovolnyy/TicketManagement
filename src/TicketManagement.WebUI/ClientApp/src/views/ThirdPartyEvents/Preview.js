import React, { Component, Fragment } from 'react'
import './Preview.css'
import { Container } from 'reactstrap'
import { withTranslation } from 'react-i18next'
import { FormatDateTime } from '../../helpers/FormatDateTime'
import { DataNavigation } from 'react-data-navigation'
import { EventManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { Auth } from '../../helpers/Auth'
import { withRouter } from '../../helpers/withRouter'

class PreviewPlain extends Component {
  static displayName = PreviewPlain.name;

  handleAddSubmit(event, ThirdPartyEvent) {
    event.preventDefault();

    let ThirdPartyEvents = DataNavigation.getData('thirdPartyEvents');
    ThirdPartyEvents.splice(ThirdPartyEvents.indexOf(String(ThirdPartyEvent)) - 1, 1)
    const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
    EventClient.apiEventManagementEventPost(ThirdPartyEvent.Price, JSON.stringify({
      name: ThirdPartyEvent.Name,
      eventTime: ThirdPartyEvent.EventTime,
      description: ThirdPartyEvent.Description,
      layoutId: ThirdPartyEvent.LayoutId,
      eventEndTime: ThirdPartyEvent.EventEndTime,
      eventLogoImage: ThirdPartyEvent.EventLogoImage
    }),
      {
        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(this.props.auth?.accessToken) },
        withCredentials: true
      }).then(response => {
        if (!response.status === 200 || !response.status === 204) {
          console.log(response);
        }
      });

    this.props.router.navigate('/ThirdPartyEvents/Preview', { replace: true });
  }

  handleDeleteSubmit(event, ThirdPartyEvent) {
    event.preventDefault();

    const { t } = this.props;
    let ThirdPartyEvents = DataNavigation.getData('thirdPartyEvents');
    const conf = window.confirm(t('Are you sure you want to delete this ThirdPartyEvent?'));
    if (conf) {
      ThirdPartyEvents.splice(ThirdPartyEvents.indexOf(String(ThirdPartyEvent)) - 1, 1)
      this.props.router.navigate('/ThirdPartyEvents/Preview', { replace: true });
    }
  }

  render() {
    const { t } = this.props;
    let ThirdPartyEvents = DataNavigation.getData('thirdPartyEvents');
    let tempId = 0;
    return (
      <Fragment>
        < div className="text-sm-center" >
          <h5 className="display-4">{t('Total ThirdPartyEvents to Add:')} {ThirdPartyEvents.length}</h5>
        </div >

        <Container>
          <div className="row">
            {ThirdPartyEvents?.map(ThirdPartyEvent => (
              (tempId++),
              <div className="col" key={"col" + tempId} >
                <form className="form-horizontal" key={"form1_" + tempId} >
                  <div className="thirdPartyEvent_img__container visible" key={"img__container" + tempId}>
                    <img className="thirdPartyEvent_image shadow-lg" alt="" src={(ThirdPartyEvent.EventLogoImage)} key={"shadow" + tempId} />
                    <div className="thirdPartyEvent_img__description" key={"img__description" + tempId} >
                      <div className="thirdPartyEvent_text" key={"text1_" + tempId} >
                        <div className="thirdPartyEvent_img__header right" key={"img__headerR" + tempId} >{FormatDateTime(ThirdPartyEvent.EventTime)}</div>
                        <div className="thirdPartyEvent_img__header left" key={"img__headerL" + tempId} >
                          <h2 className="none__text_decoration" key={"none__text_decoration" + tempId}>{ThirdPartyEvent.Name}</h2>
                        </div>
                      </div>
                      <div className="thirdPartyEvent_text" key={"text2_" + tempId}>
                        <div className="left" key={"left1_" + tempId}>
                          <p key={"p1_" + tempId}>{ThirdPartyEvent.Price}</p>
                        </div>
                        <div className="right" key={"right1_" + tempId}>
                          <p key={"p2_" + tempId}> {ThirdPartyEvent.LayoutId}</p>
                        </div>
                      </div>
                      <div className="thirdPartyEvent_text" key={"text3_" + tempId}>
                        <div className="right" key={"right2_" + tempId}>
                          <p className="font__size" key={"p3_" + tempId}>{t('Layout')}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="thirdPartyEvent_text" key={"text4_" + tempId}>
                    <p key={"p4_" + tempId}>{ThirdPartyEvent.Description}</p>
                  </div>
                  <input className="btn btn-sm btn-primary" type="button" onClick={(event) => this.handleAddSubmit(event, ThirdPartyEvent)} value={t('Add')} key={"btnAdd" + tempId} />
                </form>
                <form key={"form2_" + tempId}>
                  <button key={"btnDelete" + tempId} type="submit" className="btn btn-sm btn-danger" onClick={(event) => this.handleDeleteSubmit(event, ThirdPartyEvent)} >{t('Delete')}</button>
                </form>
              </div>
            ))}
          </div>
        </Container >
      </Fragment>
    );
  }
}

export const ThirdPartyEvents = Auth(withRouter(withTranslation()(PreviewPlain)));
