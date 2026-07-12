---
name: TaskFlow
colors:
  surface: '#f8f9ff'
  surface-dim: '#cbdbf5'
  surface-bright: '#f8f9ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#eff4ff'
  surface-container: '#e5eeff'
  surface-container-high: '#dce9ff'
  surface-container-highest: '#d3e4fe'
  on-surface: '#0b1c30'
  on-surface-variant: '#464555'
  inverse-surface: '#213145'
  inverse-on-surface: '#eaf1ff'
  outline: '#777587'
  outline-variant: '#c7c4d8'
  surface-tint: '#4d44e3'
  primary: '#3525cd'
  on-primary: '#ffffff'
  primary-container: '#4f46e5'
  on-primary-container: '#dad7ff'
  inverse-primary: '#c3c0ff'
  secondary: '#565e74'
  on-secondary: '#ffffff'
  secondary-container: '#dae2fd'
  on-secondary-container: '#5c647a'
  tertiary: '#7e3000'
  on-tertiary: '#ffffff'
  tertiary-container: '#a44100'
  on-tertiary-container: '#ffd2be'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#e2dfff'
  primary-fixed-dim: '#c3c0ff'
  on-primary-fixed: '#0f0069'
  on-primary-fixed-variant: '#3323cc'
  secondary-fixed: '#dae2fd'
  secondary-fixed-dim: '#bec6e0'
  on-secondary-fixed: '#131b2e'
  on-secondary-fixed-variant: '#3f465c'
  tertiary-fixed: '#ffdbcc'
  tertiary-fixed-dim: '#ffb695'
  on-tertiary-fixed: '#351000'
  on-tertiary-fixed-variant: '#7b2f00'
  background: '#f8f9ff'
  on-background: '#0b1c30'
  surface-variant: '#d3e4fe'
typography:
  display:
    fontFamily: Inter
    fontSize: 36px
    fontWeight: '700'
    lineHeight: 44px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Inter
    fontSize: 28px
    fontWeight: '600'
    lineHeight: 36px
    letterSpacing: -0.01em
  headline-md:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-md:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '600'
    lineHeight: 16px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Inter
    fontSize: 11px
    fontWeight: '500'
    lineHeight: 14px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 4px
  xs: 4px
  sm: 8px
  md: 16px
  lg: 24px
  xl: 32px
  gutter: 24px
  margin-mobile: 16px
  margin-desktop: 40px
---

## Brand & Style
The design system is anchored in a philosophy of **functional clarity** and **utilitarian precision**. Targeted at high-performance teams and individual power users, the UI facilitates deep work by removing visual noise. The aesthetic is a refined **Corporate Modern** style: a synthesis of systematic structure and humanist accessibility. It prioritizes information density without sacrificing legibility, using generous whitespace and a rigid alignment to the grid to evoke a sense of calm, professional reliability.

## Colors
The palette is dominated by a clean, light-neutral foundation to maximize contrast and reduce cognitive load. 
- **Primary (Deep Indigo):** Used exclusively for primary actions, active states, and critical brand touchpoints.
- **Surface & Background:** The application lives on `#F8FAFC`, with white (`#FFFFFF`) reserved for elevated containers like cards and modals to create subtle depth.
- **Semantic Accents:** A rigorous semantic system is used for task priority. **High** priority utilizes a sharp Rose to demand attention, **Medium** uses a warm Amber for caution, and **Low** utilizes Slate to recede into the background.
- **Typography & Borders:** Text uses a tiered scale of Slate grays, ensuring WCAG AA compliance for all body and label text.

## Typography
This design system utilizes **Inter** across all levels to maintain a systematic and neutral tone. The typographic scale is built on a tight 4px baseline grid. 
- **Headlines:** Use a slightly tighter letter-spacing and heavier weights to create a strong visual anchor for page sections.
- **Body:** The default size is 14px (`body-md`) for high-density dashboard views, ensuring maximum data visibility. 16px is reserved for long-form content or settings pages.
- **Labels:** Used for metadata, tags, and priority indicators. These often utilize semi-bold or medium weights to remain legible at smaller scales.

## Layout & Spacing
The layout follows a **Fluid Grid** model with a max-width of 1440px for desktop content.
- **The 8px Rule:** All spacing between elements (margins, padding, gaps) must be a multiple of 8px (or 4px for tight component internals).
- **Grid:** A 12-column grid is used for desktop, collapsing to 4 columns for mobile. 
- **Sectioning:** Large page sections are separated by `xl` (32px) spacing, while related items within a card or list use `sm` (8px) or `md` (16px) gaps.

## Elevation & Depth
This design system avoids heavy shadows and complex gradients, opting for **Tonal Layers** and **Low-Contrast Outlines**.
- **Flat Surface:** The base background is the lowest level.
- **Raised Surface:** Cards and containers use a white background with a subtle `1px` border in `#E2E8F0`. 
- **Subtle Elevation:** A single, soft shadow is used only for interactive elements that float above the UI, such as dropdown menus and modals (Shadow: `0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)`).
- **Interaction:** Hover states on rows or buttons should involve a slight background tint change rather than an increase in shadow depth.

## Shapes
A consistent `8px` (rounded-md) corner radius is applied to all primary containers, including buttons, input fields, and cards. This radius strikes a balance between professional rigidity and modern approachability. 
- **Small Elements:** Tooltips and checkboxes use a smaller `4px` radius.
- **Pills:** Priority indicators and status chips use a fully rounded (pill) shape to distinguish them from interactive buttons.

## Components
- **Buttons:**
  - *Primary:* Solid Deep Indigo with white text. No gradient.
  - *Secondary:* White background with a Slate border and Slate text.
  - *Ghost:* No border or background, only text; used for secondary actions in headers.
- **Statistic Cards:** Large `display` typography for metrics, paired with a `label-md` description. Contained in a white card with an 8px radius and a 1px Slate-200 border.
- **Task Rows:** High-density horizontal layouts. Use a hover state of `#F1F5F9`. Priority is indicated by a pill-shaped badge on the right, using the semantic color palette with a 10% opacity background and a solid text color.
- **Input Fields:** 8px radius, 1px border. Focus state uses a 2px Deep Indigo ring with a 2px offset to ensure accessibility.
- **Checkboxes:** Square with a 4px radius. When checked, they fill with the Primary color and show a white checkmark.