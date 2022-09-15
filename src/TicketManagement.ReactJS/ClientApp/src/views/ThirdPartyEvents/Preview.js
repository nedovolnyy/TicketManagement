import React, { Component } from "react"
import { Container, Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"
import './Preview.css'

class PreviewPlain extends Component {
  static displayName = PreviewPlain.name;
  constructor(props){
    super()
    this.thirdPartyEvents = props.thirdPartyEvents;
  }
  
  componentDidMount(){
    this.getThirdPartyEvents();
  }

  getThirdPartyEvents () {
    //const thirdPartyEvents = React.useContext();
  }

  render() {
    const { t } = this.props;
    return (
      <>
        < div className="text-sm-center" >
          <h5 className="display-4">{t('Total ThirdPartyEvents to Add:')} </h5>
        </div >

        <Container>
          <div className="row">
            <div className="col">
              <Form /*"ThirdPartyEvents/Add"*/ method="post" className="form-horizontal" role="form">
                <div className="img__container visible">
                  <img className="image shadow-lg" alt="" src='{thirdPartyEvent.EventLogoImage}' />
                  <div className="img__description">
                    <div className="text">
                      <div className="img__header right">Html.ModifyDateTime(thirdPartyEvent.EventTime)</div>
                      <div className="img__header left"><h2 className="none__text_decoration">@thirdPartyEvent.Name</h2></div>
                    </div>
                    <div className="text">
                      <div className="left">
                        <p>thirdPartyEvent.Price</p>
                      </div>
                      <div className="right">
                        <p>thirdPartyEvent.LayoutId</p>
                      </div>
                    </div>
                    <div className="text">
                      <div className="right">
                        <p className="font__size">{t('Layout')}</p>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="text">
                  <p>thirdPartyEvent.Description</p>
                </div>
                <input className="btn btn-sm btn-primary" type="submit" value={t('Add')} />
              </Form>
              <Form /*"ThirdPartyEvents/Delete:thirdPartyEvent.Name:@thirdPartyEvent.EventTime:@thirdPartyEvent.Description"*/  method="post">
                <Button type="submit" className="btn btn-sm btn-danger" onClick={() => { window.confirm('Are you sure you want to delete this ThirdPartyEvent?') }} >{t('Delete')}</Button>
              </Form>
            </div>
          </div>
        </Container >
      </>
    );
  }
}

export const ThirdPartyEvents = withTranslation()(PreviewPlain);
