import moment from 'moment'
import 'moment-timezone'

export function FormatDateTime(dateTime){
  return moment(dateTime).format('h:mm A M/d/yyyy')
}
