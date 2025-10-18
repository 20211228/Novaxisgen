"use strict";
var KTDatatablesBasicBasic = function() {

	var initTable1 = function() {
		var table = $('#kt_table_3');

		// begin first table
		table.DataTable({
			responsive: true,
			paging: false,
			processing: true,
            serverSide: true,

			// DOM Layout settings
			dom: `
			<'row'<'col-sm-12 col-md-5'l><'col-sm-12 col-md-7 dataTables_pager'f>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'p>>`,

			lengthMenu: [5, 10, 25, 50, 100],

			pageLength: 10,

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			"ajax": {
				"url": "https://genesis-tech.net/",
				"type": "GET"
			},
			"columns": [
				{ "data": "formatted_data_created_at" },
				{ "data": "pontos" },
				{ "data": "formatted_perna" },
				{ "data": "tipo_log" },
				{ "data": "usuario_id_de.login" },
				{ "data": "id_de" }
			],

			// Order settings
			order: [[0, 'desc']],

			columnDefs: [
			],
		});

		table.on('change', '.group-checkable', function() {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function() {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function() {
			$(this).parents('tr').toggleClass('active');
		});
	};

	var initTable2 = function() {
		var table = $('#kt_table_2');

		// begin first table
		table.DataTable({
			responsive: true,
			paging: true,

			// DOM Layout settings
			dom: `
			<'row'<'col-sm-12 col-md-5'l><'col-sm-12 col-md-7 dataTables_pager'f>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'p>>`,

			lengthMenu: [5, 10, 25, 50, 100],

			pageLength: 10,

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			// Order settings
			order: [[0, 'desc']],

			columnDefs: [
				{
					targets: 2,
					width: '200px',
					render: function(data, type, full, meta) {
						var status = {
							10: {'title': 'Direct Indication', 'state': 'primary'},
							14: {'title': 'Direct Upgrade Indication', 'state': 'primary'},
							16: {'title': 'Indirect Indication', 'state': 'primary'},
							20: {'title': 'bonus residual', 'state': 'primary'},
							25: {'title': 'ROI Bonus', 'state': 'primary'},
							28: {'title': 'Residual ROI Bonus', 'state': 'primary'},
							50: {'title': 'Binary Bonus', 'state': 'primary'},
							55: {'title': 'Qualification Bonus', 'state': 'primary'},
							80: {'title': 'ROI Withdrawal', 'state': 'warning'},
							81: {'title': 'ROI Withdrawal (CANCELED)', 'state': 'white-inverse'},
							85: {'title': 'Bonus Withdrawal', 'state': 'warning'},
							86: {'title': 'Bonus Withdrawal (CANCELED)', 'state': 'white-inverse'},
							91: {'title': 'Order Payment', 'state': 'warning'},
							90: {'title': 'Order Payment', 'state': 'warning'},
							99: {'title': 'Director Bonus', 'state': 'primary'}
						};
						if (typeof status[data] === 'undefined') {
							return data;
						}
						return '<span class="menu-bullet me-2"><span class="mb-1 bullet bullet-dot text-bg-' + status[data].state + '"></span></span>' +
							'<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
					},
				},
			],
		});

		table.on('change', '.group-checkable', function() {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function() {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function() {
			$(this).parents('tr').toggleClass('active');
		});
	};

	var initTable4 = function() {
		var table = $('#kt_table_4');

		// begin first table
		table.DataTable({
			responsive: true,
			paging: true,

			// DOM Layout settings
			dom: `
			<'row'<'col-sm-12 col-md-5'l><'col-sm-12 col-md-7 dataTables_pager'f>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'p>>`,

			lengthMenu: [5, 10, 25, 50, 100],

			pageLength: 10,

			language: {
				'lengthMenu': 'Display _MENU_',
			},

			// Order settings
			order: [[0, 'desc']],

			columnDefs: [
				{
					targets: 2,
					width: '200px',
					render: function(data, type, full, meta) {
						var status = {
							10: {'title': 'Direct Indication', 'state': 'primary'},
							14: {'title': 'Direct Upgrade Indication', 'state': 'primary'},
							16: {'title': 'Indirect Indication', 'state': 'primary'},
							20: {'title': 'bonus residual', 'state': 'primary'},
							30: {'title': 'Activation Bonus', 'state': 'primary'},
							28: {'title': 'Residual ROI Bonus', 'state': 'primary'},
							50: {'title': 'Binary Bonus', 'state': 'primary'},
							55: {'title': 'Qualification Bonus', 'state': 'primary'},
							80: {'title': 'ROI Withdrawal', 'state': 'warning'},
							81: {'title': 'ROI Withdrawal (CANCELED)', 'state': 'dark'},
							85: {'title': 'Bonus Withdrawal', 'state': 'warning'},
							86: {'title': 'Bonus Withdrawal (CANCELED)', 'state': 'dark'},
							92: {'title': 'Order Payment', 'state': 'warning'},
						};
						if (typeof status[data] === 'undefined') {
							return data;
						}
						return '<span class="label label-' + status[data].state + ' label-dot mr-2"></span>' +
							'<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
					},
				},
			],
		});

		table.on('change', '.group-checkable', function() {
			var set = $(this).closest('table').find('td:first-child .checkable');
			var checked = $(this).is(':checked');

			$(set).each(function() {
				if (checked) {
					$(this).prop('checked', true);
					$(this).closest('tr').addClass('active');
				}
				else {
					$(this).prop('checked', false);
					$(this).closest('tr').removeClass('active');
				}
			});
		});

		table.on('change', 'tbody tr .checkbox', function() {
			$(this).parents('tr').toggleClass('active');
		});
	};

	return {

		//main function to initiate the module
		init: function() {
			initTable1();
			initTable2();
			initTable4();
		}
	};
}();

jQuery(document).ready(function() {
	KTDatatablesBasicBasic.init();
});
