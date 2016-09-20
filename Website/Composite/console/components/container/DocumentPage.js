import { connect } from 'react-redux';
import FormPage from 'console/components/presentation/FormPage.js';
import { saveValues } from 'console/state/actions/values.js';
import { updateFieldValue } from 'console/state/reducers/dataFields.js';
import { fireAction } from 'console/state/actions/fireAction.js';

// Sets up a page that allows editing of a document consisting of sets of fields.
function mapStateToProps(state) {
	let update = {
		buttonDefs: state.buttonDefs,
		fieldsetDefs: state.fieldsetDefs,
		dataFieldDefs: state.dataFieldDefs,
		values: {}
	};
	Object.keys(state.dataFields).forEach(fieldName => {
		update.values[fieldName] = state.dataFields[fieldName];
	});
	update.dirtyPages = state.dataFields && state.dataFields.dirtyPages || [];
	return update;
}

function mapDispatchToProps(dispatch) {
	return {
		actions: {
			save: pageName => () => dispatch(saveValues(pageName)),
			updateValue: (pageName, fieldName) => value => dispatch(updateFieldValue(pageName, fieldName, value)),
			fireAction: (pageName, actionId) => values => dispatch(fireAction(pageName, actionId, values))
		}
	};
}

const DocumentPage = connect(mapStateToProps, mapDispatchToProps)(FormPage);

export default DocumentPage;