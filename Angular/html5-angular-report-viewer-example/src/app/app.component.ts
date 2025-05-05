import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { TelerikReportViewerComponent, TelerikReportingModule } from '@progress/telerik-angular-report-viewer';
import { StringResources } from './stringResources';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TelerikReportingModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements AfterViewInit {
  @ViewChild('viewer1') public viewer!: TelerikReportViewerComponent;

  ngAfterViewInit(): void {
      // Localization demo.
      const language = navigator.language;
      let resources = StringResources.english; // Default.

      if (language === 'ja-JP') {
          resources = StringResources.japanese;
      }

      this.viewer.viewerObject.stringResources = Object.assign(this.viewer.viewerObject.stringResources, resources);
  }

  title = "Report Viewer";
  viewerContainerStyle = {
      position: 'absolute',
      inset: '5px',
      overflow: 'hidden',
      clear: 'both',
      ['font-family']: 'ms sans serif'
  };

  ready() {
      console.log('ready');
  }
  viewerToolTipOpening(e: any, args: any) {
      console.log('viewerToolTipOpening ' + args.toolTip.text);
  }
}

