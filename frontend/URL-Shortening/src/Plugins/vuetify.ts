import { createVuetify } from 'vuetify';
import 'vuetify/styles';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import type { useBackgroundColor } from 'vuetify/lib/composables/color.mjs';

export default createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'mainTheme',
    themes: {
      mainTheme: {
          colors: {
          primary: '#87ceeb',
          secondary: '#d60000ff',
          tertiary: '#4caf50'
        }
      },
    }
  },
  defaults: {
    VAppBar: {
        color: 'primary',
        class: 'custom-app-bar',
    },
    VTextField: {
      color: 'primary',
      class: 'custom-text-field',
    },
    VAlertSuccess: {
      color: 'success',
      icon: "$success",
      class: 'custom-alert-success',
    },
    VAlertError: {
        color: 'error',
        icon: '$error',
        class: 'custom-alert-error',
    }
  }
});