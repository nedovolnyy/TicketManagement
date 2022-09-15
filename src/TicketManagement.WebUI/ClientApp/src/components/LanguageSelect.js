import React, { Component } from 'react';
import { withTranslation } from 'react-i18next';
import { UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

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
		const { t, i18n } = this.props;
		return (
			<>
				<label htmlFor='languageSelector'>
					{t('Language')}:
				</label>
				<div id='languageSelector'>
					<UncontrolledDropdown nav inNavbar className="col-md-4">
						<DropdownToggle nav caret >
							{this.renderSwitch(this.state.lang)}
						</DropdownToggle>
						<DropdownMenu end>
							<DropdownItem onClick={this.handleLanguageChange.bind(this, "en")}>
								English(en-EN)
							</DropdownItem>
							<DropdownItem onClick={this.handleLanguageChange.bind(this, "ru")}>
								Русский язык(ru-RU)
							</DropdownItem>
							<DropdownItem onClick={this.handleLanguageChange.bind(this, "by")}>
								Беларуская мова(be-BY)
							</DropdownItem>
						</DropdownMenu>
					</UncontrolledDropdown>
				</div>
			</>
		);
	}
}

export const LanguageSelect = withTranslation()(LanguageSelectPlain)
