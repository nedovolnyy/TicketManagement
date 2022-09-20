import { useEffect, useState } from 'react'

export const ThirdPartyEvent = (Component) => {
  function ComponentWithThirdPartyEventProp(props) {
    let [thirdPartyEvent, setThirdPartyEvent] = useState([]);

    useEffect(() => {
      (() => {
        
      const reader = new FileReader();
      reader.onload = (event) => {
        this.setState({ thirdPartyEvents: JSON.parse(event.target.result) })
      }
      reader.readAsText(event.target.files[0]);
      })();
    },[]);

    useEffect {
      const reader = new FileReader();
      reader.onload = (event) => {
        this.setState({ thirdPartyEvents: JSON.parse(event.target.result) })
      }
      reader.readAsText(event.target.files[0]);
    }
    return (
      <Component
        {...props}
        thirdPartyEvent={thirdPartyEvent}
      />
    );
  }
  return ComponentWithThirdPartyEventProp;
}
