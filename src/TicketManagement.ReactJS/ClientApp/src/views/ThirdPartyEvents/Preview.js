import React, { Component } from "react"
import { Container, Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"
import './Preview.css'
import { DataNavigation } from 'react-data-navigation';

class PreviewPlain extends Component {
  static displayName = PreviewPlain.name;
  constructor() {
    super()
    this.state = DataNavigation.getData('ThirdPartyEvents');
  }

  render() {
    const { t } = this.props;
    var ThirdPartyEvents = JSON.parse(this.state.result);

    return (
      <>
        < div className="text-sm-center" >
          <h5 className="display-4">{t('Total ThirdPartyEvents to Add:')} {ThirdPartyEvents.length}</h5>
        </div >

        <Container>
          <div className="row">

            {ThirdPartyEvents.map(ThirdPartyEvent => (
              <div className="col" key={"col" + ThirdPartyEvents.Id} >
                <Form /*"ThirdPartyEvents/Add"*/ method="post" className="form-horizontal" role="form" key={"form1_" + ThirdPartyEvents.Id} >
                  <div className="img__container visible" key={"img__container" + ThirdPartyEvents.Id}>
                    <img className="image shadow-lg" alt="" src={(ThirdPartyEvent.EventLogoImage)} key={"shadow" + ThirdPartyEvents.Id} />
                    <div className="img__description" key={"img__description" + ThirdPartyEvents.Id} >
                      <div className="text" key={"text1_" + ThirdPartyEvents.Id} >
                        <div className="img__header right" key={"img__headerR" + ThirdPartyEvents.Id} >{ThirdPartyEvent.EventTime}</div>
                        <div className="img__header left" key={"img__headerL" + ThirdPartyEvents.Id} >
                          <h2 className="none__text_decoration" key={"none__text_decoration" + ThirdPartyEvents.Id}>{ThirdPartyEvent.Name}</h2>
                        </div>
                      </div>
                      <div className="text" key={"text2_" + ThirdPartyEvents.Id}>
                        <div className="left" key={"left1_" + ThirdPartyEvents.Id}>
                          <p key={"p1_" + ThirdPartyEvents.Id}>{ThirdPartyEvent.Price}</p>
                        </div>
                        <div className="right" key={"right1_" + ThirdPartyEvents.Id}>
                          <p key={"p2_" + ThirdPartyEvents.Id}> {ThirdPartyEvent.LayoutId}</p>
                        </div>
                      </div>
                      <div className="text" key={"text3_" + ThirdPartyEvents.Id}>
                        <div className="right" key={"right2_" + ThirdPartyEvents.Id}>
                          <p className="font__size" key={"p3_" + ThirdPartyEvents.Id}>{t('Layout')}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="text" key={"text4_" + ThirdPartyEvents.Id}>
                    <p key={"p4_" + ThirdPartyEvents.Id}>{ThirdPartyEvent.Description}</p>
                  </div>
                  <input className="btn btn-sm btn-primary" type="submit" value={t('Add')} key={"btnAdd" + ThirdPartyEvents.Id} />
                </Form>
                <Form /*"ThirdPartyEvents/Delete:thirdPartyEvent.Name:@thirdPartyEvent.EventTime:@thirdPartyEvent.Description"*/ method="post" key={"form2_" + ThirdPartyEvents.Id}>
                  <Button key={"btnDelete" + ThirdPartyEvents.Id} type="submit" className="btn btn-sm btn-danger" onClick={() => { window.confirm('Are you sure you want to delete this ThirdPartyEvent?') }} >{t('Delete')}</Button>
                </Form>
              </div>
            ))}
          </div>
        </Container >
      </>
    );
  }
}

export const ThirdPartyEvents = withTranslation()(PreviewPlain);
