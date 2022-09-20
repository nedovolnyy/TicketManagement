import React, { Component } from "react"
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
        </>
      )
  }
}
