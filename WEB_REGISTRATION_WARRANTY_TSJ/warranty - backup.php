



<!doctype html>
<html lang="en">
    <head>
    
        <meta charset="utf-8">
        <title>Update Registration TSJ</title>
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

		<?php
			$registration_code = isset($_GET['RegistrationCode']) ? $_GET['RegistrationCode'] : 'Guest'; // Default to 'Guest' if not set
		
		?>

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
                            <h3 class="page-title"></h3>
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
									
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label"># Form Registrasi</label>
											
										</div>
										<br/>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Activation Code</label>
											<input type="text" class="form-control" id="ActivationCode" readOnly>
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Product Number</label>
											<input type="text" class="form-control" id="ProductNumber" readOnly>
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Serial Number</label>
											<input type="text" class="form-control" id="SerialNumber" readOnly>
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Nama Barang</label>
											<input type="text" class="form-control" id="ProductName" maxlength="150">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Registration Code</label>
											<input type="text" class="form-control" id="RegistrationCode" maxlength="50">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Nama</label>
											<input type="text" class="form-control" id="CustomerName" maxlength="50">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Tempat Lahir</label>
											<input type="text" class="form-control" id="PlaceOfBirth" maxlength="50">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Tanggal Lahir</label>
											<input type="date" class="form-control" id="DateOfBirth">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">No. Telepon</label>
											<input type="number" class="form-control" id="Telephone" maxlength="15">
										</div>
										<div class="form-group">
											<label for="inputText" class="col-sm-5 col-form-label">Email</label>
											<input type="email" class="form-control" id="Email" maxlength="50">
										</div>
										<br/>
										<div class="form-group input-group-outline">
											<button type="button" class="btn btn-info" style="font-size:16px;" onclick="SaveDataWarranty();">Save</button>
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

		LoadDataWarranty();
		function LoadDataWarranty() {
		    var url = "https://localhost:7214/api/Warranty/ListGetDataWarrantyRegistration?registrationCode=<?php echo $registration_code; ?>";
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

					console.log(JSON.stringify(data));
					if (data.error == false) {
						
						document.getElementById("ActivationCode").value = data.activationCode;
						document.getElementById("ProductNumber").value = data.productCode;
						document.getElementById("SerialNumber").value = data.serialCode;
						document.getElementById("ProductName").value = "";
						document.getElementById("RegistrationCode").value = "<?php echo $registration_code; ?>";
						document.getElementById("CustomerName").value = "";
						document.getElementById("PlaceOfBirth").value = "";
						document.getElementById("DateOfBirth").value = "";
						document.getElementById("Telephone").value = "";
						document.getElementById("Email").value = "";
					}
					else {

						document.getElementById("ActivationCode").value = "";
						document.getElementById("ProductNumber").value = "";
						document.getElementById("SerialNumber").value = "";
						document.getElementById("ProductName").value = "";
						document.getElementById("RegistrationCode").value = "";
						document.getElementById("CustomerName").value = "";
						document.getElementById("PlaceOfBirth").value = "";
						document.getElementById("DateOfBirth").value = "";
						document.getElementById("Telephone").value = "";
						document.getElementById("Email").value = "";
					}
				},
				error: function (xhr, status, error) {

					
				}

			});
		}
		
    </script>
</html>
