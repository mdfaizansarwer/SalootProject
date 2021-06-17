<template>
  <div class="wrapper">
    <!-- Navbar -->
    <nav class="main-header navbar navbar-expand navbar-white navbar-light">
      <!-- Left navbar links -->
      <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link" data-widget="pushmenu" role="button"
            ><i class="fas fa-bars"></i
          ></a>
        </li>
        <li class="nav-item d-none d-sm-inline-block">
          <a class="nav-link">Home</a>
        </li>
        <li class="nav-item d-none d-sm-inline-block">
          <a class="nav-link">Contact</a>
        </li>
      </ul>

      <!-- Right navbar links -->
      <ul class="navbar-nav ml-auto">
        <li class="nav-item">
          <a class="nav-link" data-widget="fullscreen" role="button">
            <i class="fas fa-expand-arrows-alt"></i>
          </a>
        </li>
      </ul>
    </nav>
    <!-- /.navbar -->

    <!-- Main Sidebar Container -->
    <aside class="main-sidebar sidebar-dark-primary elevation-4">
      <!-- Brand Logo -->
      <a class="brand-link">
        <img
          src="../../node_modules/admin-lte/dist/img/AdminLTELogo.png"
          alt="AdminLTE Logo"
          class="brand-image img-circle elevation-3"
          style="opacity: 0.8"
        />
        <span class="brand-text font-weight-light">Saloot Project</span>
      </a>

      <!-- Sidebar -->
      <div class="sidebar">
        <!-- Sidebar user panel (optional) -->
        <div class="user-panel mt-3 pb-3 mb-3 d-flex">
          <div class="image">
            <img
              src="../../node_modules/admin-lte/dist/img/user2-160x160.jpg"
              class="img-circle elevation-2"
              alt="User Image"
            />
          </div>
          <div class="info">
            <a class="d-block">Alexander Pierce</a>
          </div>
        </div>

        <!-- SidebarSearch Form -->
        <div class="form-inline">
          <div class="input-group" data-widget="sidebar-search">
            <input
              class="form-control form-control-sidebar"
              type="search"
              placeholder="Search"
              aria-label="Search"
            />
            <div class="input-group-append">
              <button class="btn btn-sidebar">
                <i class="fas fa-search fa-fw"></i>
              </button>
            </div>
          </div>
        </div>

        <!-- Sidebar Menu -->
        <nav class="mt-2">
          <ul
            class="nav nav-pills nav-sidebar flex-column"
            data-widget="treeview"
            role="menu"
            data-accordion="false"
          >
            <!-- Add icons to the links using the .nav-icon class
               with font-awesome or any other icon font library -->
            <li class="nav-item menu-open">
              <a class="nav-link active">
                <i class="nav-icon fas fa-tachometer-alt"></i>
                <p>
                  Tickets
                  <i class="right fas fa-angle-left"></i>
                </p>
              </a>
              <ul class="nav nav-treeview">
                <li class="nav-item">
                  <a class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Create</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Your Tickets</p>
                  </a>
                </li>
              </ul>
            </li>

            <li class="nav-item">
              <a class="nav-link">
                <i class="nav-icon far fa-envelope"></i>
                <p>
                  Profile
                  <i class="fas fa-angle-left right"></i>
                </p>
              </a>
              <ul class="nav nav-treeview">
                <li class="nav-item">
                  <a
                    class="nav-link"
                    @click="
                      renderComponent('Account Setting', 'AccountSetting')
                    "
                  >
                    <i class="far fa-circle nav-icon"></i>
                    <p>Account Setting</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Sign out</p>
                  </a>
                </li>
              </ul>
            </li>
          </ul>
        </nav>
        <!-- /.sidebar-menu -->
      </div>
      <!-- /.sidebar -->
    </aside>

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
      <!-- Content Header (Page header) -->
      <div class="content-header">
        <div class="container-fluid">
          <div class="row mb-2">
            <div class="col-sm-6">
              <h1 class="m-0">{{ contentHeader }}</h1>
            </div>
          </div>
          <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
      </div>
      <!-- /.content-header -->

      <!-- Main content -->
      <div class="content">
        <div class="container-fluid">
          <template>
            <component v-bind:is="currentComponent" />
          </template>
        </div>
        <!-- /.container-fluid -->
      </div>
      <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->

    <!-- Main Footer -->
    <footer class="main-footer">
      <strong
        >Copyright &copy; 2014-2021
        <a href="https://adminlte.io">AdminLTE.io</a>.</strong
      >
      All rights reserved.
      <div class="float-right d-none d-sm-inline-block">
        <b>Version</b> 3.1.0
      </div>
    </footer>
  </div>
  <!-- ./wrapper -->
</template>

<script lang="ts">

// jQuery
import "../../node_modules/admin-lte/plugins/jquery/jquery.min.js";
// Bootstrap 
import "../../node_modules/admin-lte/plugins/bootstrap/js/bootstrap.bundle.min.js";
// AdminLTE
import "../../node_modules/admin-lte/dist/js/adminlte.js";
// // OPTIONAL SCRIPTS
// import  "../../node_modules/admin-lte/plugins/chart.js/Chart.min.js";
//       // AdminLTE for demo purpose
// import  "../../node_modules/admin-lte/dist/js/demo.js";
//       // AdminLTE dashboard demo (This is only for demo purposes
// import  "../../node_modules/admin-lte/dist/js/pages/dashboard3.js";

import { Component, Vue } from "vue-property-decorator";
import AccountSetting from "@/components/AccountSetting.vue";

@Component({
  components: {
    AccountSetting
  },

  data() {
    return {
      contentHeader: "Welcome To Us 😀",
      currentComponent: null
    }
  },
})

export default class AdminPanel extends Vue {
  mounted(): void {
    document.body.classList.add("hold-transition", "sidebar-mini");
  }

  renderComponent(contentHeader: string, currentComponent: string): void {
    this.$data.contentHeader = contentHeader;
    this.$data.currentComponent = currentComponent;
  }
}
</script>

<style scoped>
/* Google Font: Source Sans Pro */
@import url("https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback");

/* Font Awesome Icons  */
@import "../../node_modules/admin-lte/plugins/fontawesome-free/css/all.min.css";

/* IonIcons */ /* 404 File not found */
/* @import url("https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.cs"); */

/* Theme style */
@import "../../node_modules/admin-lte/dist/css/adminlte.min.css";
</style>
