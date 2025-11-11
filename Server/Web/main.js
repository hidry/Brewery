"use strict";
(self["webpackChunkbrewery"] = self["webpackChunkbrewery"] || []).push([["main"],{

/***/ 92:
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   AppComponent: () => (/* binding */ AppComponent)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ 2596);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ 8431);
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/card */ 3777);



class AppComponent {
  constructor() {
    this.title = 'Brauanlage';
  }
  static {
    this.ɵfac = function AppComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || AppComponent)();
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
      type: AppComponent,
      selectors: [["app-root"]],
      standalone: false,
      decls: 9,
      vars: 0,
      consts: [["routerLink", "/boilingPlate1", "routerLinkActive", "active"], ["routerLink", "/boilingPlate2", "routerLinkActive", "active"], ["routerLink", "/log", "routerLinkActive", "active"]],
      template: function AppComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "mat-card")(1, "nav")(2, "a", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3, "Heizplatte 1");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "a", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](5, "Heizplatte 2");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "a", 2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](7, "Client-Log");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](8, "router-outlet");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }
      },
      dependencies: [_angular_router__WEBPACK_IMPORTED_MODULE_1__.RouterOutlet, _angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterLink, _angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterLinkActive, _angular_material_card__WEBPACK_IMPORTED_MODULE_3__.MatCard],
      styles: ["/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 635:
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   AppModule: () => (/* binding */ AppModule)
/* harmony export */ });
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/platform-browser */ 9736);
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./app-routing.module */ 4114);
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./app.component */ 92);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/forms */ 4456);
/* harmony import */ var _messages_messages_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./messages/messages.component */ 6706);
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/common/http */ 9648);
/* harmony import */ var ag_grid_angular__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ag-grid-angular */ 7291);
/* harmony import */ var _mash_steps_mash_steps_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./mash-steps/mash-steps.component */ 8760);
/* harmony import */ var _boiling_plate2_boiling_plate2_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./boiling-plate2/boiling-plate2.component */ 6976);
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/platform-browser/animations */ 3835);
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/card */ 3777);
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/icon */ 3840);
/* harmony import */ var _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/slide-toggle */ 8827);
/* harmony import */ var _angular_material_slider__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/slider */ 4992);
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/material/checkbox */ 7024);
/* harmony import */ var _boiling_plate1_boiling_plate1_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./boiling-plate1/boiling-plate1.component */ 2638);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/core */ 7580);

















class AppModule {
  static {
    this.ɵfac = function AppModule_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || AppModule)();
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdefineNgModule"]({
      type: AppModule,
      bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_1__.AppComponent]
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdefineInjector"]({
      imports: [_angular_platform_browser__WEBPACK_IMPORTED_MODULE_7__.BrowserModule, _app_routing_module__WEBPACK_IMPORTED_MODULE_0__.AppRoutingModule, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.FormsModule, _angular_common_http__WEBPACK_IMPORTED_MODULE_9__.HttpClientModule, ag_grid_angular__WEBPACK_IMPORTED_MODULE_10__.AgGridModule, _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_11__.BrowserAnimationsModule, _angular_material_card__WEBPACK_IMPORTED_MODULE_12__.MatCardModule, _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__.MatIconModule, _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_14__.MatSlideToggleModule, _angular_material_slider__WEBPACK_IMPORTED_MODULE_15__.MatSliderModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_16__.MatCheckboxModule
      // ,
      // // The HttpClientInMemoryWebApiModule module intercepts HTTP requests
      // // and returns simulated server responses.
      // // Remove it when a real server is ready to receive requests.
      // HttpClientInMemoryWebApiModule.forRoot(
      //   InMemoryDataService, { dataEncapsulation: false }
      // )
      ]
    });
  }
}
(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵsetNgModuleScope"](AppModule, {
    declarations: [_app_component__WEBPACK_IMPORTED_MODULE_1__.AppComponent, _messages_messages_component__WEBPACK_IMPORTED_MODULE_2__.MessagesComponent, _mash_steps_mash_steps_component__WEBPACK_IMPORTED_MODULE_3__.MashStepsComponent, _boiling_plate2_boiling_plate2_component__WEBPACK_IMPORTED_MODULE_4__.BoilingPlate2Component, _boiling_plate1_boiling_plate1_component__WEBPACK_IMPORTED_MODULE_5__.BoilingPlate1Component],
    imports: [_angular_platform_browser__WEBPACK_IMPORTED_MODULE_7__.BrowserModule, _app_routing_module__WEBPACK_IMPORTED_MODULE_0__.AppRoutingModule, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.FormsModule, _angular_common_http__WEBPACK_IMPORTED_MODULE_9__.HttpClientModule, ag_grid_angular__WEBPACK_IMPORTED_MODULE_10__.AgGridModule, _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_11__.BrowserAnimationsModule, _angular_material_card__WEBPACK_IMPORTED_MODULE_12__.MatCardModule, _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__.MatIconModule, _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_14__.MatSlideToggleModule, _angular_material_slider__WEBPACK_IMPORTED_MODULE_15__.MatSliderModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_16__.MatCheckboxModule
    // ,
    // // The HttpClientInMemoryWebApiModule module intercepts HTTP requests
    // // and returns simulated server responses.
    // // Remove it when a real server is ready to receive requests.
    // HttpClientInMemoryWebApiModule.forRoot(
    //   InMemoryDataService, { dataEncapsulation: false }
    // )
    ]
  });
})();

/***/ }),

/***/ 2638:
/*!************************************************************!*\
  !*** ./src/app/boiling-plate1/boiling-plate1.component.ts ***!
  \************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   BoilingPlate1Component: () => (/* binding */ BoilingPlate1Component)
/* harmony export */ });
/* harmony import */ var _mash_steps_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../mash-steps.service */ 6350);
/* harmony import */ var _boiling_plate1_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../boiling-plate1.service */ 9623);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ 9240);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ 3037);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ 6647);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../settings */ 2809);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/router */ 8431);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/forms */ 4456);
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/icon */ 3840);
/* harmony import */ var _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/slide-toggle */ 8827);













class BoilingPlate1Component {
  constructor(mashStepsService, boilingPlate1Service, settings) {
    this.mashStepsService = mashStepsService;
    this.boilingPlate1Service = boilingPlate1Service;
    this.settings = settings;
  }
  ngOnInit() {
    // this.getCurrentStep();
    this.currentStepSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_3__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_5__.switchMap)(() => this.mashStepsService.getCurrentMashStep())).subscribe(mashStep => {
      this.CurrentStepName = mashStep.Step;
      this.CurrentStepEstimatedRemainingTime = mashStep.EstimatedTime;
    });
    // this.getTotalEstimatedRemainingTime();
    this.totalEstimatedRemainingTimeSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_3__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_5__.switchMap)(() => this.mashStepsService.getTotalEstimatedRemainingTime())).subscribe(t => this.TotalEstimatedRemainingTime = Math.round(t));
    // this.getCurrentTemperature();
    this.currentTemperatureSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_3__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_5__.switchMap)(() => this.boilingPlate1Service.getCurrentTemperature())).subscribe(ct => this.TemperatureCurrent = Math.round(ct));
    // this.getPowerStatus();
    this.powerStatusSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_3__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_5__.switchMap)(() => this.boilingPlate1Service.getPowerStatus())).subscribe(p => this.Power = p);
  }
  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.totalEstimatedRemainingTimeSubscription.unsubscribe();
    this.currentStepSubscription.unsubscribe();
  }
  // getPowerStatus(): void {
  //   this.boilingPlate1Service.getPowerStatus().subscribe(p => this.Power = p);
  // }
  // getCurrentTemperature(): void {
  //   this.boilingPlate1Service.getCurrentTemperature().subscribe(ct => this.TemperatureCurrent = ct);
  // }
  // getTotalEstimatedRemainingTime(): void {
  //   this.mashStepsService.getTotalEstimatedRemainingTime()
  //     .subscribe(t => this.TotalEstimatedRemainingTime = t);
  // }
  // getCurrentStep(): void {
  //   this.mashStepsService.getCurrentMashStep()
  //     .subscribe(mashStep => {
  //       this.CurrentStepName = mashStep.Step;
  //       const elapsedTime = mashStep.ElapsedMinutes === undefined ? mashStep.Rast : mashStep.ElapsedMinutes;
  //       this.CurrentStepEstimatedRemainingTime = mashStep.Rast - elapsedTime;
  //     });
  // }
  onPowerToggleChange(event) {
    if (event.checked) {
      this.boilingPlate1Service.start().subscribe();
    } else {
      this.boilingPlate1Service.stop().subscribe();
    }
  }
  acknowledgeMessage() {
    this.boilingPlate1Service.acknowledgeMessage().subscribe();
  }
  static {
    this.ɵfac = function BoilingPlate1Component_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || BoilingPlate1Component)(_angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdirectiveInject"](_mash_steps_service__WEBPACK_IMPORTED_MODULE_0__.MashStepsService), _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdirectiveInject"](_boiling_plate1_service__WEBPACK_IMPORTED_MODULE_1__.BoilingPlate1Service), _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdirectiveInject"](_settings__WEBPACK_IMPORTED_MODULE_2__.Settings));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵdefineComponent"]({
      type: BoilingPlate1Component,
      selectors: [["app-boiling-plate1"]],
      standalone: false,
      decls: 17,
      vars: 5,
      consts: [["routerLink", "/mashSteps"], [3, "ngModelChange", "change", "ngModel"], ["mat-button", "", 3, "click"]],
      template: function BoilingPlate1Component_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](0, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](1, " Einstellungen: ");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](2, "a", 0)(3, "mat-icon");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](4, "settings");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]()()();
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](5, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](6, " Steuerung: ");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](7, "mat-slide-toggle", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtwoWayListener"]("ngModelChange", function BoilingPlate1Component_Template_mat_slide_toggle_ngModelChange_7_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtwoWayBindingSet"](ctx.Power, $event) || (ctx.Power = $event);
            return $event;
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵlistener"]("change", function BoilingPlate1Component_Template_mat_slide_toggle_change_7_listener($event) {
            return ctx.onPowerToggleChange($event);
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]()();
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](8, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](9);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](10, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](11);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](12, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](13);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementStart"](14, "p")(15, "button", 2);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵlistener"]("click", function BoilingPlate1Component_Template_button_click_15_listener() {
            return ctx.acknowledgeMessage();
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtext"](16, "Nachricht best\u00E4tigen");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵelementEnd"]()();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵadvance"](7);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtwoWayProperty"]("ngModel", ctx.Power);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtextInterpolate1"](" Temperatur aktuell: ", ctx.TemperatureCurrent, " \u00B0C\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtextInterpolate2"](" ", ctx.CurrentStepName, " noch ca. ", ctx.CurrentStepEstimatedRemainingTime, " Minuten\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_6__["ɵɵtextInterpolate1"](" Gesamt noch ca. ", ctx.TotalEstimatedRemainingTime, " Minuten\n");
        }
      },
      dependencies: [_angular_router__WEBPACK_IMPORTED_MODULE_7__.RouterLink, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgModel, _angular_material_icon__WEBPACK_IMPORTED_MODULE_9__.MatIcon, _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_10__.MatSlideToggle],
      styles: ["/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 2809:
/*!*****************************!*\
  !*** ./src/app/settings.ts ***!
  \*****************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   Settings: () => (/* binding */ Settings)
/* harmony export */ });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ 9648);
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../environments/environment */ 5312);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ 7580);



class Settings {
  constructor() {
    this.ApiUrl = _environments_environment__WEBPACK_IMPORTED_MODULE_0__.environment.apiUrl;
    this.httpOptions = {
      headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__.HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    this.pollingInterval = 5000;
    this.clientLogActive = false;
  }
  static {
    this.ɵfac = function Settings_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || Settings)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjectable"]({
      token: Settings,
      factory: Settings.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 3852:
/*!************************************!*\
  !*** ./src/app/message.service.ts ***!
  \************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   MessageService: () => (/* binding */ MessageService)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 7580);

class MessageService {
  constructor() {
    this.messages = [];
  }
  add(message) {
    this.messages.push(new Date().toLocaleTimeString() + ' - ' + message);
  }
  clear() {
    this.messages = [];
  }
  static {
    this.ɵfac = function MessageService_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || MessageService)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
      token: MessageService,
      factory: MessageService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 4114:
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   AppRoutingModule: () => (/* binding */ AppRoutingModule)
/* harmony export */ });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ 8431);
/* harmony import */ var _mash_steps_mash_steps_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./mash-steps/mash-steps.component */ 8760);
/* harmony import */ var _boiling_plate2_boiling_plate2_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./boiling-plate2/boiling-plate2.component */ 6976);
/* harmony import */ var _boiling_plate1_boiling_plate1_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./boiling-plate1/boiling-plate1.component */ 2638);
/* harmony import */ var _messages_messages_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./messages/messages.component */ 6706);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ 7580);







const routes = [{
  path: '',
  component: _boiling_plate1_boiling_plate1_component__WEBPACK_IMPORTED_MODULE_2__.BoilingPlate1Component,
  pathMatch: 'full'
}, {
  path: 'log',
  component: _messages_messages_component__WEBPACK_IMPORTED_MODULE_3__.MessagesComponent
}, {
  path: 'mashSteps',
  component: _mash_steps_mash_steps_component__WEBPACK_IMPORTED_MODULE_0__.MashStepsComponent
}, {
  path: 'boilingPlate1',
  component: _boiling_plate1_boiling_plate1_component__WEBPACK_IMPORTED_MODULE_2__.BoilingPlate1Component
}, {
  path: 'boilingPlate2',
  component: _boiling_plate2_boiling_plate2_component__WEBPACK_IMPORTED_MODULE_1__.BoilingPlate2Component
}];
class AppRoutingModule {
  static {
    this.ɵfac = function AppRoutingModule_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || AppRoutingModule)();
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineNgModule"]({
      type: AppRoutingModule
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineInjector"]({
      imports: [_angular_router__WEBPACK_IMPORTED_MODULE_5__.RouterModule.forRoot(routes), _angular_router__WEBPACK_IMPORTED_MODULE_5__.RouterModule]
    });
  }
}
(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵsetNgModuleScope"](AppRoutingModule, {
    imports: [_angular_router__WEBPACK_IMPORTED_MODULE_5__.RouterModule],
    exports: [_angular_router__WEBPACK_IMPORTED_MODULE_5__.RouterModule]
  });
})();

/***/ }),

/***/ 4429:
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/platform-browser */ 9736);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./app/app.module */ 635);
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./environments/environment */ 5312);




if (_environments_environment__WEBPACK_IMPORTED_MODULE_1__.environment.production) {
  (0,_angular_core__WEBPACK_IMPORTED_MODULE_2__.enableProdMode)();
}
_angular_platform_browser__WEBPACK_IMPORTED_MODULE_3__.platformBrowser().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_0__.AppModule).catch(err => console.error(err));

/***/ }),

/***/ 5312:
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   environment: () => (/* binding */ environment)
/* harmony export */ });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
const environment = {
  production: false,
  apiUrl: 'http://localhost:8800/api'
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.

/***/ }),

/***/ 5398:
/*!*******************************************!*\
  !*** ./src/app/boiling-plate2.service.ts ***!
  \*******************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   BoilingPlate2Service: () => (/* binding */ BoilingPlate2Service)
/* harmony export */ });
/* harmony import */ var _message_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./message.service */ 3852);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ 8764);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ 1318);
/* harmony import */ var _serviceBase__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./serviceBase */ 9976);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./settings */ 2809);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/common/http */ 9648);










class BoilingPlate2Service extends _serviceBase__WEBPACK_IMPORTED_MODULE_1__.ServiceBase {
  constructor(http, messageService, settings) {
    super(messageService, settings);
    this.http = http;
    this.endpointUrl = `${this.settings.ApiUrl}/boilingPlate2`;
  }
  getPowerStatus() {
    const url = `${this.endpointUrl}/powerStatus`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched PowerStatus')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getPowerStatus')));
  }
  /** GET currentTemperature from the server */
  getCurrentTemperature() {
    const url = `${this.endpointUrl}/getCurrentTemperature`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched currentTemperature')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getCurrentTemperature')));
  }
  getTemperature() {
    const url = `${this.endpointUrl}/getTemperature`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched Temperature')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getTemperature')));
  }
  power(power) {
    const url = `${this.endpointUrl}/power/${power}`;
    return this.http.put(url, null, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`put power ${power}`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('power')));
  }
  setTemperature(temperature) {
    const url = `${this.endpointUrl}/setTemperature/${temperature}`;
    return this.http.put(url, null, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`put temperature ${temperature}`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('setTemperature')));
  }
  static {
    this.ɵfac = function BoilingPlate2Service_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || BoilingPlate2Service)(_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_6__.HttpClient), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_message_service__WEBPACK_IMPORTED_MODULE_0__.MessageService), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_settings__WEBPACK_IMPORTED_MODULE_2__.Settings));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineInjectable"]({
      token: BoilingPlate2Service,
      factory: BoilingPlate2Service.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 6350:
/*!***************************************!*\
  !*** ./src/app/mash-steps.service.ts ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   MashStepsService: () => (/* binding */ MashStepsService)
/* harmony export */ });
/* harmony import */ var _message_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./message.service */ 3852);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ 8764);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ 1318);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./settings */ 2809);
/* harmony import */ var _serviceBase__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./serviceBase */ 9976);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/common/http */ 9648);










class MashStepsService extends _serviceBase__WEBPACK_IMPORTED_MODULE_2__.ServiceBase {
  constructor(http, messageService, settings) {
    super(messageService, settings);
    this.http = http;
    this.endpointUrl = `${this.settings.ApiUrl}/mashSteps`;
  }
  /** GET mashSteps from the server */
  getMashSteps() {
    return this.http.get(this.endpointUrl).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched mashSteps')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getMashSteps', [])));
  }
  /** GET currentMashStep from the server */
  getCurrentMashStep() {
    const url = `${this.endpointUrl}/currentStep`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched currentStep')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getCurrentMashStep')));
  }
  /** GET totalEstimatedRemainingTime from the server */
  getTotalEstimatedRemainingTime() {
    const url = `${this.endpointUrl}/totalEstimatedRemainingTime`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched totalEstimatedRemainingTime')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getTotalEstimatedRemainingTime')));
  }
  updateMashStep(mashStep) {
    return this.http.put(this.endpointUrl, mashStep, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`updated mashStep guid=${mashStep.Guid}`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('updateMashStep')));
  }
  /** DELETE: delete the mashStep from the server */
  deleteMashStep(mashStep) {
    const url = `${this.endpointUrl}/${mashStep.Guid}`;
    return this.http.delete(url, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`deleted mashStep guid=${mashStep.Guid}`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('deleteMashStep')));
  }
  /** POST: add a new MashStep to the server */
  addMashStep(mashStep) {
    return this.http.post(this.endpointUrl, mashStep, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(ms => this.log(`added mashStep guid=${ms.Guid}`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('addMashStep')));
  }
  static {
    this.ɵfac = function MashStepsService_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || MashStepsService)(_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_6__.HttpClient), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_message_service__WEBPACK_IMPORTED_MODULE_0__.MessageService), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_settings__WEBPACK_IMPORTED_MODULE_1__.Settings));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineInjectable"]({
      token: MashStepsService,
      factory: MashStepsService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 6706:
/*!************************************************!*\
  !*** ./src/app/messages/messages.component.ts ***!
  \************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   MessagesComponent: () => (/* binding */ MessagesComponent)
/* harmony export */ });
/* harmony import */ var _message_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../message.service */ 3852);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../settings */ 2809);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common */ 4460);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ 4456);
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/checkbox */ 7024);








function MessagesComponent_div_3_div_4_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementStart"](0, "div");
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const message_r3 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵadvance"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtextInterpolate1"](" ", message_r3, " ");
  }
}
function MessagesComponent_div_3_Template(rf, ctx) {
  if (rf & 1) {
    const _r1 = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementStart"](0, "div")(1, "p")(2, "button", 2);
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵlistener"]("click", function MessagesComponent_div_3_Template_button_click_2_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵrestoreView"](_r1);
      const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵresetView"](ctx_r1.messageService.clear());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtext"](3, "clear");
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtemplate"](4, MessagesComponent_div_3_div_4_Template, 2, 1, "div", 3);
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵproperty"]("ngForOf", ctx_r1.messageService.messages.slice().reverse());
  }
}
class MessagesComponent {
  constructor(messageService, settings) {
    this.messageService = messageService;
    this.settings = settings;
  }
  ngOnInit() {}
  static {
    this.ɵfac = function MessagesComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || MessagesComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdirectiveInject"](_message_service__WEBPACK_IMPORTED_MODULE_0__.MessageService), _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdirectiveInject"](_settings__WEBPACK_IMPORTED_MODULE_1__.Settings));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineComponent"]({
      type: MessagesComponent,
      selectors: [["app-messages"]],
      standalone: false,
      decls: 4,
      vars: 3,
      consts: [[3, "ngModelChange", "ngModel", "checked"], [4, "ngIf"], [1, "clear", 3, "click"], [4, "ngFor", "ngForOf"]],
      template: function MessagesComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementStart"](0, "p")(1, "mat-checkbox", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtwoWayListener"]("ngModelChange", function MessagesComponent_Template_mat_checkbox_ngModelChange_1_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtwoWayBindingSet"](ctx.settings.clientLogActive, $event) || (ctx.settings.clientLogActive = $event);
            return $event;
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtext"](2, "Aktiv");
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵelementEnd"]()();
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtemplate"](3, MessagesComponent_div_3_Template, 5, 1, "div", 1);
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵadvance"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵtwoWayProperty"]("ngModel", ctx.settings.clientLogActive);
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵproperty"]("checked", ctx.settings.clientLogActive);
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵproperty"]("ngIf", ctx.messageService.messages.length);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_3__.NgForOf, _angular_common__WEBPACK_IMPORTED_MODULE_3__.NgIf, _angular_forms__WEBPACK_IMPORTED_MODULE_4__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_4__.NgModel, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_5__.MatCheckbox],
      styles: ["\n\nh2[_ngcontent-%COMP%] {\n    color: red;\n    font-family: Arial, Helvetica, sans-serif;\n    font-weight: lighter;\n  }\n  body[_ngcontent-%COMP%] {\n    margin: 2em;\n  }\n  body[_ngcontent-%COMP%], input[text][_ngcontent-%COMP%], button[_ngcontent-%COMP%] {\n    color: crimson;\n    font-family: Cambria, Georgia;\n  }\n  \n  button.clear[_ngcontent-%COMP%] {\n    font-family: Arial;\n    background-color: #eee;\n    border: none;\n    padding: 5px 10px;\n    border-radius: 4px;\n    cursor: pointer;\n    cursor: hand;\n  }\n  button[_ngcontent-%COMP%]:hover {\n    background-color: #cfd8dc;\n  }\n  button[_ngcontent-%COMP%]:disabled {\n    background-color: #eee;\n    color: #aaa;\n    cursor: auto;\n  }\n  button.clear[_ngcontent-%COMP%] {\n    color: #888;\n    margin-bottom: 12px;\n  }\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8uL3NyYy9hcHAvbWVzc2FnZXMvbWVzc2FnZXMuY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSwyQ0FBMkM7QUFDM0M7SUFDSSxVQUFVO0lBQ1YseUNBQXlDO0lBQ3pDLG9CQUFvQjtFQUN0QjtFQUNBO0lBQ0UsV0FBVztFQUNiO0VBQ0E7SUFDRSxjQUFjO0lBQ2QsNkJBQTZCO0VBQy9COztFQUVBO0lBQ0Usa0JBQWtCO0lBQ2xCLHNCQUFzQjtJQUN0QixZQUFZO0lBQ1osaUJBQWlCO0lBQ2pCLGtCQUFrQjtJQUNsQixlQUFlO0lBQ2YsWUFBWTtFQUNkO0VBQ0E7SUFDRSx5QkFBeUI7RUFDM0I7RUFDQTtJQUNFLHNCQUFzQjtJQUN0QixXQUFXO0lBQ1gsWUFBWTtFQUNkO0VBQ0E7SUFDRSxXQUFXO0lBQ1gsbUJBQW1CO0VBQ3JCIiwic291cmNlc0NvbnRlbnQiOlsiLyogTWVzc2FnZXNDb21wb25lbnQncyBwcml2YXRlIENTUyBzdHlsZXMgKi9cbmgyIHtcbiAgICBjb2xvcjogcmVkO1xuICAgIGZvbnQtZmFtaWx5OiBBcmlhbCwgSGVsdmV0aWNhLCBzYW5zLXNlcmlmO1xuICAgIGZvbnQtd2VpZ2h0OiBsaWdodGVyO1xuICB9XG4gIGJvZHkge1xuICAgIG1hcmdpbjogMmVtO1xuICB9XG4gIGJvZHksIGlucHV0W3RleHRdLCBidXR0b24ge1xuICAgIGNvbG9yOiBjcmltc29uO1xuICAgIGZvbnQtZmFtaWx5OiBDYW1icmlhLCBHZW9yZ2lhO1xuICB9XG4gIFxuICBidXR0b24uY2xlYXIge1xuICAgIGZvbnQtZmFtaWx5OiBBcmlhbDtcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiAjZWVlO1xuICAgIGJvcmRlcjogbm9uZTtcbiAgICBwYWRkaW5nOiA1cHggMTBweDtcbiAgICBib3JkZXItcmFkaXVzOiA0cHg7XG4gICAgY3Vyc29yOiBwb2ludGVyO1xuICAgIGN1cnNvcjogaGFuZDtcbiAgfVxuICBidXR0b246aG92ZXIge1xuICAgIGJhY2tncm91bmQtY29sb3I6ICNjZmQ4ZGM7XG4gIH1cbiAgYnV0dG9uOmRpc2FibGVkIHtcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiAjZWVlO1xuICAgIGNvbG9yOiAjYWFhO1xuICAgIGN1cnNvcjogYXV0bztcbiAgfVxuICBidXR0b24uY2xlYXIge1xuICAgIGNvbG9yOiAjODg4O1xuICAgIG1hcmdpbi1ib3R0b206IDEycHg7XG4gIH0iXSwic291cmNlUm9vdCI6IiJ9 */"]
    });
  }
}

/***/ }),

/***/ 6976:
/*!************************************************************!*\
  !*** ./src/app/boiling-plate2/boiling-plate2.component.ts ***!
  \************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   BoilingPlate2Component: () => (/* binding */ BoilingPlate2Component)
/* harmony export */ });
/* harmony import */ var _boiling_plate2_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../boiling-plate2.service */ 5398);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ 9240);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../settings */ 2809);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ 3037);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ 6647);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ 4456);
/* harmony import */ var _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material/slide-toggle */ 8827);
/* harmony import */ var _angular_material_slider__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/slider */ 4992);










class BoilingPlate2Component {
  constructor(boilingPlate2Service, settings) {
    this.boilingPlate2Service = boilingPlate2Service;
    this.settings = settings;
  }
  ngOnInit() {
    // this.getTemperature();
    this.temperatureSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_2__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.switchMap)(() => this.boilingPlate2Service.getTemperature())).subscribe(t => this.Temperature = Math.round(t));
    // this.getCurrentTemperature();
    this.currentTemperatureSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_2__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.switchMap)(() => this.boilingPlate2Service.getCurrentTemperature())).subscribe(ct => this.TemperatureCurrent = Math.round(ct));
    // this.getPowerStatus();
    this.powerStatusSubscription = (0,rxjs__WEBPACK_IMPORTED_MODULE_2__.interval)(this.settings.pollingInterval).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.startWith)(0), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.switchMap)(() => this.boilingPlate2Service.getPowerStatus())).subscribe(p => this.Power = p);
  }
  ngOnDestroy() {
    this.powerStatusSubscription.unsubscribe();
    this.currentTemperatureSubscription.unsubscribe();
    this.temperatureSubscription.unsubscribe();
  }
  // getTemperature() {
  //   this.boilingPlate2Service.getTemperature().subscribe(t => this.Temperature = t);
  // }
  // getPowerStatus(): void {
  //   this.boilingPlate2Service.getPowerStatus().subscribe(p => this.Power = p);
  // }
  // getCurrentTemperature(): void {
  //   this.boilingPlate2Service.getCurrentTemperature().subscribe(ct => this.TemperatureCurrent = ct);
  // }
  onPowerToggleChange(event) {
    this.boilingPlate2Service.power(event.checked).subscribe();
  }
  onTemperatureSliderChange(event) {
    this.boilingPlate2Service.setTemperature(event.value).subscribe();
  }
  static {
    this.ɵfac = function BoilingPlate2Component_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || BoilingPlate2Component)(_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdirectiveInject"](_boiling_plate2_service__WEBPACK_IMPORTED_MODULE_0__.BoilingPlate2Service), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdirectiveInject"](_settings__WEBPACK_IMPORTED_MODULE_1__.Settings));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineComponent"]({
      type: BoilingPlate2Component,
      selectors: [["app-boiling-plate2"]],
      standalone: false,
      decls: 9,
      vars: 4,
      consts: [[3, "ngModelChange", "change", "ngModel"], ["thumbLabel", "true", 3, "input", "ngModelChange", "ngModel"]],
      template: function BoilingPlate2Component_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementStart"](0, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtext"](1, " Steuerung: ");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementStart"](2, "mat-slide-toggle", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayListener"]("ngModelChange", function BoilingPlate2Component_Template_mat_slide_toggle_ngModelChange_2_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayBindingSet"](ctx.Power, $event) || (ctx.Power = $event);
            return $event;
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵlistener"]("change", function BoilingPlate2Component_Template_mat_slide_toggle_change_2_listener($event) {
            return ctx.onPowerToggleChange($event);
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementEnd"]()();
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementStart"](3, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtext"](4, " Temperatur soll: ");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementStart"](5, "mat-slider", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵlistener"]("input", function BoilingPlate2Component_Template_mat_slider_input_5_listener($event) {
            return ctx.onTemperatureSliderChange($event);
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayListener"]("ngModelChange", function BoilingPlate2Component_Template_mat_slider_ngModelChange_5_listener($event) {
            _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayBindingSet"](ctx.Temperature, $event) || (ctx.Temperature = $event);
            return $event;
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtext"](6);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementStart"](7, "p");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtext"](8);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵelementEnd"]();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayProperty"]("ngModel", ctx.Power);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵadvance"](3);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtwoWayProperty"]("ngModel", ctx.Temperature);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵadvance"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtextInterpolate1"](" ", ctx.Temperature, " \u00B0C\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵtextInterpolate1"](" Temperatur aktuell: ", ctx.TemperatureCurrent, " \u00B0C\n");
        }
      },
      dependencies: [_angular_forms__WEBPACK_IMPORTED_MODULE_6__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.NgModel, _angular_material_slide_toggle__WEBPACK_IMPORTED_MODULE_7__.MatSlideToggle, _angular_material_slider__WEBPACK_IMPORTED_MODULE_8__.MatSlider],
      styles: ["/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 8760:
/*!****************************************************!*\
  !*** ./src/app/mash-steps/mash-steps.component.ts ***!
  \****************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   MashStepsComponent: () => (/* binding */ MashStepsComponent)
/* harmony export */ });
/* harmony import */ var _mashStep__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../mashStep */ 9035);
/* harmony import */ var _mash_steps_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../mash-steps.service */ 6350);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ 1873);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var ag_grid_angular__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ag-grid-angular */ 7291);






class MashStepsComponent {
  constructor(mashStepsService) {
    this.mashStepsService = mashStepsService;
    this.trueFalseValues = ['true', 'false'];
    this.trueFalseCellEditorParams = {
      values: this.trueFalseValues
    };
    this.columnDefs = [{
      headerName: 'Schritt',
      field: 'Step',
      editable: true,
      checkboxSelection: true
    }, {
      headerName: 'Rührwerk',
      field: 'Mixer',
      editable: true,
      cellEditor: 'select',
      cellEditorParams: this.trueFalseCellEditorParams
    }, {
      headerName: 'Alarm',
      field: 'Alert',
      editable: true,
      cellEditor: 'select',
      cellEditorParams: this.trueFalseCellEditorParams
    }, {
      headerName: 'Temperatur',
      field: 'Temperature',
      editable: true,
      valueParser: 'Number(newValue)'
    }, {
      headerName: 'Rast',
      field: 'Rast',
      editable: true,
      valueParser: 'Number(newValue)'
    }];
  }
  ngOnInit() {
    this.getMashSteps();
  }
  // on grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params) {
    this.api = params.api;
    this.columnApi = params.columnApi;
    this.api.sizeColumnsToFit();
  }
  rowsSelected() {
    return this.api && this.api.getSelectedRows().length > 0;
  }
  getMashSteps() {
    this.mashStepsService.getMashSteps().subscribe(mashSteps => this.mashSteps = mashSteps);
  }
  onCellValueChanged(params) {
    this.mashStepsService.updateMashStep(params.data).subscribe(ms => {
      // console.log('MashStep Saved');
      this.getMashSteps();
    }, error => console.log(error));
  }
  deleteSelectedRows() {
    const selectRows = this.api.getSelectedRows();
    // create an Observable for each row to delete
    const deleteSubscriptions = selectRows.map(rowToDelete => {
      return this.mashStepsService.deleteMashStep(rowToDelete);
    });
    // then subscribe to these and once all done, refresh the grid data
    // deleteSubscriptions.forkJoin().subscribe(results => this.getMashSteps());
    (0,rxjs__WEBPACK_IMPORTED_MODULE_2__.forkJoin)(...deleteSubscriptions).subscribe(results => this.getMashSteps());
  }
  addNewMashStep() {
    const newMashStep = new _mashStep__WEBPACK_IMPORTED_MODULE_0__.MashStep();
    newMashStep.Step = 'new step';
    newMashStep.Active = false;
    newMashStep.Rast = 60;
    this.mashStepsService.addMashStep(newMashStep).subscribe(r => this.getMashSteps());
  }
  static {
    this.ɵfac = function MashStepsComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || MashStepsComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdirectiveInject"](_mash_steps_service__WEBPACK_IMPORTED_MODULE_1__.MashStepsService));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineComponent"]({
      type: MashStepsComponent,
      selectors: [["app-mash-steps"]],
      standalone: false,
      decls: 5,
      vars: 3,
      consts: [["id", "api", "suppressRowClickSelection", "", "rowSelection", "multiple", 1, "ag-theme-balham", 2, "width", "100%", "height", "250px", 3, "gridReady", "cellValueChanged", "rowData", "columnDefs"], ["mat-button", "", 3, "click", "disabled"], ["mat-button", "", 3, "click"]],
      template: function MashStepsComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementStart"](0, "ag-grid-angular", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵlistener"]("gridReady", function MashStepsComponent_Template_ag_grid_angular_gridReady_0_listener($event) {
            return ctx.onGridReady($event);
          })("cellValueChanged", function MashStepsComponent_Template_ag_grid_angular_cellValueChanged_0_listener($event) {
            return ctx.onCellValueChanged($event);
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementStart"](1, "button", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵlistener"]("click", function MashStepsComponent_Template_button_click_1_listener() {
            return ctx.deleteSelectedRows();
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵtext"](2, " Schritt(e) l\u00F6schen\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementStart"](3, "button", 2);
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵlistener"]("click", function MashStepsComponent_Template_button_click_3_listener() {
            return ctx.addNewMashStep();
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵtext"](4, " Schritt hinzuf\u00FCgen\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵelementEnd"]();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵproperty"]("rowData", ctx.mashSteps)("columnDefs", ctx.columnDefs);
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵadvance"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵproperty"]("disabled", !ctx.rowsSelected());
        }
      },
      dependencies: [ag_grid_angular__WEBPACK_IMPORTED_MODULE_4__.AgGridAngular],
      styles: ["/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 9035:
/*!*****************************!*\
  !*** ./src/app/mashStep.ts ***!
  \*****************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   MashStep: () => (/* binding */ MashStep)
/* harmony export */ });
class MashStep {}

/***/ }),

/***/ 9623:
/*!*******************************************!*\
  !*** ./src/app/boiling-plate1.service.ts ***!
  \*******************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   BoilingPlate1Service: () => (/* binding */ BoilingPlate1Service)
/* harmony export */ });
/* harmony import */ var _message_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./message.service */ 3852);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ 8764);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ 1318);
/* harmony import */ var _serviceBase__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./serviceBase */ 9976);
/* harmony import */ var _settings__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./settings */ 2809);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ 7580);
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/common/http */ 9648);










class BoilingPlate1Service extends _serviceBase__WEBPACK_IMPORTED_MODULE_1__.ServiceBase {
  constructor(http, messageService, settings) {
    super(messageService, settings);
    this.http = http;
    this.endpointUrl = `${this.settings.ApiUrl}/boilingPlate1`;
  }
  acknowledgeMessage() {
    const url = `${this.endpointUrl}/acknowledgeMessage`;
    return this.http.put(url, null, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`put acknowledgeMessage`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('acknowledgeMessage')));
  }
  start() {
    const url = `${this.endpointUrl}/startMashProcess`;
    return this.http.put(url, null, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`put startMashProcess`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('start')));
  }
  stop() {
    const url = `${this.endpointUrl}/stopMashProcess`;
    return this.http.put(url, null, this.settings.httpOptions).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log(`put stopMashProcess`)), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('stop')));
  }
  getPowerStatus() {
    const url = `${this.endpointUrl}/powerStatus`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched PowerStatus')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getPowerStatus')));
  }
  /** GET currentTemperature from the server */
  getCurrentTemperature() {
    const url = `${this.endpointUrl}/getCurrentTemperature`;
    return this.http.get(url).pipe((0,rxjs_operators__WEBPACK_IMPORTED_MODULE_3__.tap)(_ => this.log('fetched currentTemperature')), (0,rxjs_operators__WEBPACK_IMPORTED_MODULE_4__.catchError)(this.handleError('getCurrentTemperature')));
  }
  static {
    this.ɵfac = function BoilingPlate1Service_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || BoilingPlate1Service)(_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_6__.HttpClient), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_message_service__WEBPACK_IMPORTED_MODULE_0__.MessageService), _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵinject"](_settings__WEBPACK_IMPORTED_MODULE_2__.Settings));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineInjectable"]({
      token: BoilingPlate1Service,
      factory: BoilingPlate1Service.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 9976:
/*!********************************!*\
  !*** ./src/app/serviceBase.ts ***!
  \********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   ServiceBase: () => (/* binding */ ServiceBase)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ 9452);

class ServiceBase {
  constructor(messageService, settings) {
    this.messageService = messageService;
    this.settings = settings;
  }
  /** Log a MashStepsService message with the MessageService */
  log(message) {
    if (this.settings.clientLogActive) {
      this.messageService.add(`MashStepsService: ${message}`);
    }
  }
  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  handleError(operation = 'operation', result) {
    return error => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
      // Let the app keep running by returning an empty result.
      return (0,rxjs__WEBPACK_IMPORTED_MODULE_0__.of)(result);
    };
  }
}

/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor"], () => (__webpack_exec__(4429)));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=main.js.map