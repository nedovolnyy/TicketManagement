import React, { Component } from 'react'
import { withTranslation } from 'react-i18next'
import { CDropdown, CDropdownToggle, CDropdownMenu, CDropdownItem } from '@coreui/react';

export class LanguageSelectPlain extends Component {
	static displayName = LanguageSelectPlain.name;

	constructor(props) {
		super(props);

		const { i18n } = this.props;
		this.toggleNavbar = this.toggleNavbar.bind(this);
		this.state = {
			lang: i18n.language,
			collapsed: true
		};
	}

	toggleNavbar() {
		this.setState({
			collapsed: !this.state.collapsed
		});
	}

	handleLanguageChange = (lang) => {
		const { i18n } = this.props;
		i18n.changeLanguage(lang);
		this.setState({ lang: lang });
	}

	renderSwitch(param) {
		switch (param) {
			case 'by':
				return 'Беларуская мова';
			case 'en':
				return 'English';
			case 'ru':
				return 'Русский язык';
			default:
				return 'English';
		}
	}

	render() {
		const { t } = this.props;
		return (
				<div className="col-md-4">
					<CDropdown variant="btn-group" direction="dropup">
						<label>{t('Language')}:</label>
						<CDropdownToggle color="secondary">{this.renderSwitch(this.state.lang)}</CDropdownToggle>
						<CDropdownMenu>
							<CDropdownItem onClick={this.handleLanguageChange.bind(this, "en")}>English(en-EN)</CDropdownItem>
							<CDropdownItem onClick={this.handleLanguageChange.bind(this, "ru")}>Русский язык(ru-RU)</CDropdownItem>
							<CDropdownItem onClick={this.handleLanguageChange.bind(this, "by")}>Беларуская мова(be-BY)</CDropdownItem>
						</CDropdownMenu>
					</CDropdown>
				</div>
		);
	}
}

export const LanguageSelect = withTranslation()(LanguageSelectPlain)
