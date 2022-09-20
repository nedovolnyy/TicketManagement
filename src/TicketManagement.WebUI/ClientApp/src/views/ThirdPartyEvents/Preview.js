import React, { Component, Fragment } from 'react'
import { Container, Button, Form } from 'reactstrap'
import { withTranslation } from 'react-i18next'
import './Preview.css'
import { DataNavigation } from 'react-data-navigation'

class PreviewPlain extends Component {
  static displayName = PreviewPlain.name;

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
                <Form /*"ThirdPartyEvents/Add"*/ method="post" className="form-horizontal" role="form" key={"form1_" + tempId} >
                  <div className="img__container visible" key={"img__container" + tempId}>
                    <img className="image shadow-lg" alt="" src={(ThirdPartyEvent.EventLogoImage)} key={"shadow" + tempId} />
                    <div className="img__description" key={"img__description" + tempId} >
                      <div className="text" key={"text1_" + tempId} >
                        <div className="img__header right" key={"img__headerR" + tempId} >{ThirdPartyEvent.EventTime}</div>
                        <div className="img__header left" key={"img__headerL" + tempId} >
                          <h2 className="none__text_decoration" key={"none__text_decoration" + tempId}>{ThirdPartyEvent.Name}</h2>
                        </div>
                      </div>
                      <div className="text" key={"text2_" + tempId}>
                        <div className="left" key={"left1_" + tempId}>
                          <p key={"p1_" + tempId}>{ThirdPartyEvent.Price}</p>
                        </div>
                        <div className="right" key={"right1_" + tempId}>
                          <p key={"p2_" + tempId}> {ThirdPartyEvent.LayoutId}</p>
                        </div>
                      </div>
                      <div className="text" key={"text3_" + tempId}>
                        <div className="right" key={"right2_" + tempId}>
                          <p className="font__size" key={"p3_" + tempId}>{t('Layout')}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="text" key={"text4_" + tempId}>
                    <p key={"p4_" + tempId}>{ThirdPartyEvent.Description}</p>
                  </div>
                  <input className="btn btn-sm btn-primary" type="submit" value={t('Add')} key={"btnAdd" + tempId} />
                </Form>
                <Form /*"ThirdPartyEvents/Delete:thirdPartyEvent.Name:@thirdPartyEvent.EventTime:@thirdPartyEvent.Description"*/ method="post" key={"form2_" + tempId}>
                  <Button key={"btnDelete" + tempId} type="submit" className="btn btn-sm btn-danger" onClick={() => { window.confirm('Are you sure you want to delete this ThirdPartyEvent?') }} >{t('Delete')}</Button>
                </Form>
              </div>
            ))}
          </div>
        </Container >
      </Fragment>
    );
  }
}

export const ThirdPartyEvents = withTranslation()(PreviewPlain);
