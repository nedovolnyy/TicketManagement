import React, { Component } from "react"
//import { withTranslation } from "react-i18next"
import { FormatDateTime } from "../helpers/FormatDateTime"
import { SelectEventSeatPartial } from "./SelectEventSeatPartial"

export class EventAreasMap extends Component {
  constructor(props) {
    super()
  }

  render() {

    for (var i = 0; i < this.props.eventAreas.length; i++)
      return (
        <>
          <tr>
            <td>
              <div className="text">
                <div className="container">
                  <div className="container text-right">
                    <SelectEventSeatPartial eventArea={this.props.eventAreas[i]} />
                  </div>
                </div>
                <div className="right">
                  <p>{this.props.eventAreas[i].description}</p>
                </div>
              </div>
              <div className="textE">
                <div className="right">
                  <p className="eventTime">{FormatDateTime(this.props.eventTime)}</p>
                </div>
              </div>
            </td>
          </tr>
        </>
      )
  }
}

//export const EventAreasMap = withTranslation()(EventAreasMapPlain);
