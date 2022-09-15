import by from './localization/be-BY.json'
import en from './localization/en-EN.json'
import ru from './localization/ru-RU.json'

import { initReactI18next } from 'react-i18next'
import i18n from 'i18next'


const resources = {
  by: {
    translation: by,
  },
  en: {
    translation: en,
  },
  ru: {
    translation: ru,
  }
}

i18n
  .use(initReactI18next)
  .init({
    resources,
    lng: JSON.parse(localStorage.getItem('language')),
    fallbackLng: 'en'
  })

export default i18n;
