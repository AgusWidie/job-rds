
<!doctype html>
<html lang="en">
    <head>
    
        <meta charset="utf-8">
        <title>Data Warranty TSJ</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta content="Premium Multipurpose Admin & Dashboard Template" name="description">
        <meta content="Themesbrand" name="author">
        <!-- App favicon -->
        <link rel="shortcut icon" href="assets/images/favicon.ico">
    
        <!-- DataTables -->
        <link href="assets/css/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css">
        <link href="assets/css/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css">
    
        <!-- Responsive datatable examples -->
        <link href="assets/css/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css">
    
        <!-- Bootstrap Css -->
        <link href="assets/css/bootstrap.min.css" id="bootstrap-style" rel="stylesheet" type="text/css">
        <!-- Icons Css -->
        <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css">
        <!-- App Css-->
        <link href="assets/css/app.min.css" id="app-style" rel="stylesheet" type="text/css">
		
		<link href="Content/css/jquery.dataTables.min.css" rel="stylesheet">

		<script src="Content/js/jquery-3.7.0.js"></script>
		<script src="Content/js/jquery.dataTables.min.js"></script>

		<link href="Content/css/select2.min.css" rel="stylesheet">
		<script src="Content/js/select2.min.js"></script>

		<link href="Content/select2/select2-bootstrap.css" rel="stylesheet">
		<link href="Content/select2/select2.min.css" rel="stylesheet">
		<script src="Content/select2/select2.full.js"></script>
    
    </head>

    <body data-sidebar="dark">


        <!-- Loader -->
            <div id="preloader"><div id="status"><div class="spinner"></div></div></div>

        <!-- Begin page -->
        <div id="layout-wrapper">

            <header id="page-topbar">
                <div class="navbar-header">
                    <div class="d-flex">
                        <!-- LOGO -->
                        <div class="navbar-brand-box">
                         
                        </div>

                        <button type="button" class="btn btn-sm px-3 font-size-24 header-item waves-effect" id="vertical-menu-btn">
                           
                        </button>

                        <div class="d-none d-sm-block ms-2">
                            <h4 class="page-title"></h4>
                        </div>
                    </div>

                    <!-- Search input -->
                    <div class="search-wrap" id="search-wrap">
                        <div class="search-bar">
                            <input class="search-input form-control" placeholder="Search">
                            <a href="#" class="close-search toggle-search" data-target="#search-wrap">
                                <i class="mdi mdi-close-circle"></i>
                            </a>
                        </div>
                    </div>

                    <div class="d-flex">

                        <div class="dropdown d-none d-lg-inline-block me-2">
                            <button type="button" class="btn header-item toggle-search noti-icon waves-effect" data-target="#search-wrap">
                             
                            </button>
                        </div>

                        <div class="dropdown d-none d-lg-inline-block me-2">
                            <button type="button" class="btn header-item noti-icon waves-effect" data-bs-toggle="fullscreen">
                              
                            </button>
                        </div>

                        <div class="dropdown d-none d-md-block me-2">
                            <button type="button" class="btn header-item waves-effect" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                               
                            </button>
                            <div class="dropdown-menu dropdown-menu-end">

                               
                            </div>
                        </div>

                        <div class="dropdown d-inline-block me-2">
                            <button type="button" class="btn header-item noti-icon waves-effect" id="page-header-notifications-dropdown" data-bs-toggle="dropdown" aria-expanded="false">
                              
                            </button>
                            
                        </div>

                        <div class="dropdown d-inline-block">
                            <button type="button" class="btn header-item waves-effect" id="page-header-user-dropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                
                            </button>
                            <div class="dropdown-menu dropdown-menu-end">
                             
                                
                            </div>
                        </div>

                        <div class="dropdown d-inline-block">
                            <button type="button" class="btn header-item noti-icon right-bar-toggle waves-effect">
                              
                            </button>
                        </div>

                    </div>
                </div>
            </header>

            <!-- ========== Left Sidebar Start ========== -->
            <div class="vertical-menu">

                <div data-simplebar class="h-100">

                    <!--- Sidemenu -->
                    <div id="sidebar-menu">
                        <!-- Left Menu Start -->
                        <ul class="metismenu list-unstyled" id="side-menu">
							<li>
                                <a href="" class="waves-effect">
                                    <span> Menu </span>
                                </a>
                            </li>

                            <!--<li>
                                <a href="javascript: void(0);" class="waves-effect">
                                    <span class="badge rounded-pill bg-primary float-end">20+</span>
                                    <i class="mdi mdi-view-dashboard"></i>
                                    <span> Dashboard </span>
                                </a>
                                <ul class="sub-menu" aria-expanded="false">
                                    <li><a href="index.html">Dashboard One</a></li>
                                    <li><a href="dashboard-2.html">Dashboard Two</a></li>
                                </ul>
                            </li>-->

                            <li>
                                <a href="warranty.php" class="waves-effect">
                                    #
                                    <span> List Data Warranty </span>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <!-- Sidebar -->
                </div>
            </div>
            <!-- Left Sidebar End -->

            <!-- ============================================================== -->
            <!-- Start right Content here -->
            <!-- ============================================================== -->
            <div class="main-content">

                <div class="page-content">
                    <div class="row">
							<div class="col-12">
								<div class="card pb-2 min-vh-85">
									
									<div class="card-body">
										
									    
										<div class="table-responsive mb-0">
											<div class="row">
													
													<div class="col-2">

														<div class="input-group input-group-outline">
															<label for="example-color-input" class="form-control-label">Registration Code</label>
														</div>
														<div class="input-group input-group-outline">
															<input type="text" id="fRegistrationCode" name="fRegistrationCode" class="form-control" />
														</div>
													</div>
													
													<div class="col-2">

														<div class="input-group input-group-outline">
															<label for="example-color-input" class="form-control-label">Created At From</label>
														</div>
														<div class="input-group input-group-outline">
															<input type="date" id="fCreatedAtFrom" name="fCreatedAtFrom" class="form-control" format="yyyy-MM-dd" />
														</div>
													</div>
													<div class="col-2">

														<div class="input-group input-group-outline">
															<label for="example-color-input" class="form-control-label">Created At To</label>
														</div>
														<div class="input-group input-group-outline">
															<input type="date" id="fCreatedAtTo" name="fCreatedAtTo" class="form-control" format="yyyy-MM-dd" />
														</div>
													</div>
													<div class="col-4">
														<br />
														<div class="input-group input-group-outline">
															<label for="example-color-input" class="form-control-label"></label>
														</div>
														<div class="input-group input-group-outline">
															<button class="btn btn-info" onclick="SearchDataWarranty()" type="button" name="button">
																Search
															</button>&nbsp;&nbsp;
															
														</div>
													</div>
													
												</div>
											<br/>
											<br/>
											<table class="table table-flush" id="datatable-buttons" style="font-size: 14px; width: 200%;">
												<thead>
													<tr>
														<th class="text-center">Product Number</th>
														<th class="text-center">Source</th>
														<th class="text-center">Activation Code</th>
														<th class="text-center">Serial Number</th>
														<th class="text-center">Registration Code</th>
														<th class="text-center">Activation At</th>
														<th class="text-center">Activation By</th>
														<th class="text-center">Registration At</th>
														<th class="text-center">Registration By</th>
														<th class="text-center">Customer Name</th>
														<th class="text-center">Email</th>
														<th class="text-center">Place Of Birth</th>
														<th class="text-center">Date Of Birth</th>
														<th class="text-center">Gender</th>
														<th class="text-center">Telephone</th>
													</tr>
												</thead>
												<tbody>
												 
												</tbody>
											</table>
										</div>
									</div>
								</div>
							</div>
						</div>
                </div>
                <!-- End Page-content -->

                
                <footer class="footer">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-6">
                                <script>document.write(new Date().getFullYear())</script> © Warranty.
                            </div>
                            <div class="col-sm-6">
                                <div class="text-sm-end d-none d-sm-block">
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
            <!-- end main content-->

        </div>
        <!-- END layout-wrapper -->

        <!-- Right Sidebar -->
        <div class="right-bar">
            <div data-simplebar class="h-100">
                <div class="rightbar-title px-3 py-4">
                    <a href="javascript:void(0);" class="right-bar-toggle float-end">
                        <i class="mdi mdi-close noti-icon"></i>
                    </a>
                    <h5 class="m-0">Settings</h5>
                </div>

                <!-- Settings -->
                <hr class="mt-0">
                <h6 class="text-center mb-0">Choose Layouts</h6>

                <div class="p-4">
                    <div class="mb-2">
                        <img src="assets/images/layouts/layout-1.jpg" class="img-fluid img-thumbnail" alt="Layouts-1">
                    </div>
                    <div class="form-check form-switch mb-3">
                        <input class="form-check-input theme-choice" type="checkbox" id="light-mode-switch">
                        <label class="form-check-label" for="light-mode-switch">Light Mode</label>
                    </div>

                    <div class="mb-2">
                        <img src="assets/images/layouts/layout-2.jpg" class="img-fluid img-thumbnail" alt="Layouts-2">
                    </div>

                    <div class="form-check form-switch mb-3">
                        <input class="form-check-input theme-choice" type="checkbox" id="dark-mode-switch" data-bsStyle="assets/css/bootstrap-dark.min.css" data-appStyle="assets/css/app-dark.min.css">
                        <label class="form-check-label" for="dark-mode-switch">Dark Mode</label>
                    </div>
    
                    <div class="mb-2">
                        <img src="assets/images/layouts/layout-3.jpg" class="img-fluid img-thumbnail" alt="Layouts-3">
                    </div>

                    <div class="form-check form-switch mb-3">
                        <input class="form-check-input theme-choice" type="checkbox"  id="rtl-mode-switch" data-appStyle="assets/css/app-rtl.min.css">
                        <label class="form-check-label" for="rtl-mode-switch">RTL Mode</label>
                    </div>
            
            
                </div>

            </div> <!-- end slimscroll-menu-->
        </div>
        <!-- /Right-bar -->

        <!-- Right bar overlay-->
        <div class="rightbar-overlay"></div>

                             
        <!-- JAVASCRIPT -->
        <script src="assets/js/jquery.min.js"></script>
        <script src="assets/js/bootstrap.bundle.min.js"></script>
        <script src="assets/js/metisMenu.min.js"></script>
        <script src="assets/js/simplebar.min.js"></script>
        <script src="assets/js/waves.min.js"></script>

        <!-- Required datatable js -->
        <script src="assets/js/jquery.dataTables.min.js"></script>
        <script src="assets/js/dataTables.bootstrap4.min.js"></script>
        <!-- Buttons examples -->
        <script src="assets/js/dataTables.buttons.min.js"></script>
        <script src="assets/js/buttons.bootstrap4.min.js"></script>
        <script src="assets/js/jszip.min.js"></script>
        <script src="assets/js/pdfmake.min.js"></script>
        <script src="assets/js/vfs_fonts.js"></script>
        <script src="assets/js/buttons.html5.min.js"></script>
        <script src="assets/js/buttons.print.min.js"></script>
        <script src="assets/js/buttons.colVis.min.js"></script>
        <!-- Responsive examples -->
        <script src="assets/js/dataTables.responsive.min.js"></script>
        <script src="assets/js/responsive.bootstrap4.min.js"></script>

        <!-- Datatable init js -->
        <script src="assets/js/datatables.init.js"></script> 

        <script src="assets/js/app.js"></script>

    </body>
	
	<script>
		$(document).ready(function() {

			var fCreatedAtFrom = document.getElementById("fCreatedAtFrom").value;
			var fCreatedAtTo = document.getElementById("fCreatedAtTo").value;
			
			var url = "https://localhost:7214/api/Warranty/ListDataWarranty?createdAtFrom=&createdAtTo=";
			$.ajax({
				headers: {
					'Access-Control-Allow-Origin': '*',
					'Access-Control-Allow-Methods': 'GET',
					'Content-type': 'application/json; charset=utf-8',
				},
				type: "GET",
				url: url,
				dataType: "jsonp",
				success: function (data) {

					if (data.error == false) {
						var listTableWarranty = $('#datatable-buttons').DataTable( {
							"columnDefs": [
								{ "data": "productCode" },
								{ "data": "source" },
								{ "data": "activationCode" },
								{ "data": "serialNumber" },
								{ "data": "registrationCode" },
								{ "data": "activationAt" },
								{ "data": "activationBy" },
								{ "data": "registrationAt" },
								{ "data": "registrationBy" },
								{ "data": "customerName" },
								{ "data": "email" },
								{ "data": "placeOfBirth" },
								{ "data": "dateOfBirth" },
								{ "data": "gender" },
								{ "data": "telephone" }
							],
							"language":{
								"search": "",
								"searchPlaceholder": "Cari...",
								"zeroRecords": "Data Not Found.",
								"processing": "Proses..."
							  
							}
						});

					}
					else {

						
					}
				},
				error: function (xhr, status, error) {

					
				}

			});
		});
		
		function SearchDataWarranty() {
			
			var fCreatedAtFrom = document.getElementById("fCreatedAtFrom").value;
			var fCreatedAtTo = document.getElementById("fCreatedAtTo").value;
			
			if(fCreatedAtFrom == "" || fCreatedAtFrom == null) {
				alert("Created At From Empty.");
				return;
			}
			
			if(fCreatedAtTo == "" || fCreatedAtTo == null) {
				alert("Created At To Empty.");
				return;
			}
			
			var url = "https://localhost:7214/api/Warranty/ListDataWarranty?createdAtFrom=" + fCreatedAtFrom + "&createdAtTo=" + fCreatedAtTo + "";
			
			$.ajax({
				type: "GET",
				headers: {
					'Access-Control-Allow-Origin': '*',
					'Access-Control-Allow-Methods': 'GET',
					'Content-type': 'application/json; charset=utf-8',
				},
				url: url,
				dataType: "jsonp",
				success: function (data) {

					if (data.error == false) {
						var listTableWarranty = $('#datatable-buttons').DataTable( {
							"columnDefs": [
								{ "data": "productCode" },
								{ "data": "source" },
								{ "data": "activationCode" },
								{ "data": "serialNumber" },
								{ "data": "registrationCode" },
								{ "data": "activationAt" },
								{ "data": "activationBy" },
								{ "data": "registrationAt" },
								{ "data": "registrationBy" },
								{ "data": "customerName" },
								{ "data": "email" },
								{ "data": "placeOfBirth" },
								{ "data": "dateOfBirth" },
								{ "data": "gender" },
								{ "data": "telephone" }
							],
							"language":{
								"search": "",
								"searchPlaceholder": "Cari...",
								"zeroRecords": "Data Not Found.",
								"processing": "Proses..."
							  
							}
						});

					}
					else {

						
					}
				},
				error: function (xhr, status, error) {

					
				}
			});

		}
    </script>
</html>
